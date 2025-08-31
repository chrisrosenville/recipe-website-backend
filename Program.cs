using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;

using Recipes.Database;
using Recipes.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Prevent cycles when serializing EF navigation properties
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    // Emit enums as strings in JSON (e.g., "Admin" instead of 0)
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false));
});

// CORS for frontend (configurable via ALLOWED_ORIGINS env, comma-separated)
var allowedOriginsEnv = Environment.GetEnvironmentVariable("ALLOWED_ORIGINS");
var allowedOrigins = (allowedOriginsEnv?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                     ?? new[] { "http://localhost:3000" });

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});
builder.Services.AddOpenApi();

// Dependency Injection
builder.Services.AddDbContext<RecipeDbContext>(options =>
    options
        .UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_URL") ?? builder.Configuration.GetConnectionString("DefaultConnection"))
        .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning))
);

// JWT Service
builder.Services.AddScoped<JwtService>();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // Read token from cookie if not in Authorization header
            if (string.IsNullOrEmpty(context.Token))
            {
                context.Token = context.Request.Cookies["jwt"];
            }
            return Task.CompletedTask;
        }
    };

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Ensure database is created and migrations are applied on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RecipeDbContext>();
    var maxAttempts = 30;
    for (var attempt = 1; attempt <= maxAttempts; attempt++)
    {
        try
        {
            db.Database.Migrate();
            Console.WriteLine("Database migrations applied successfully.");
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Attempt {attempt}/{maxAttempts}] Database migration failed: {ex.Message}");
            if (attempt == maxAttempts)
            {
                Console.WriteLine("Giving up applying migrations. Failing fast.");
                throw;
            }
            else
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }
    }

    // Idempotent data patch: ensure seeded user has Admin role
    try
    {
        var seeded = await db.Users.FirstOrDefaultAsync(u => u.Id == 1);
        if (seeded != null)
        {
            var roles = seeded.Roles?.ToList() ?? new List<Recipes.Models.UserRole>();
            if (!roles.Contains(Recipes.Models.UserRole.Admin))
            {
                roles.Add(Recipes.Models.UserRole.Admin);
                seeded.Roles = roles.Distinct().ToArray();
                await db.SaveChangesAsync();
                Console.WriteLine("Seed user updated with Admin role.");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Warning: failed to ensure Admin role on seed user: {ex.Message}");
    }
}

app.UseHttpsRedirection();
app.UseCors("Frontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
