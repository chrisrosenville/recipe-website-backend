using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

using Recipes.Database;
using Recipes.Models;
using Recipes.Services;

namespace Recipes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(
    RecipeDbContext dbContext,
    JwtService jwtService,
    IWebHostEnvironment environment
    ) : ControllerBase
{
    private readonly RecipeDbContext _dbContext = dbContext;
    private readonly JwtService _jwtService = jwtService;
    private readonly IWebHostEnvironment _environment = environment;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUserDTO)
    {
        var existingEmail = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == registerUserDTO.Email.ToLower());
        if (existingEmail != null)
        {
            return Conflict("User with this email already exists.");
        }

        var existingDisplayName = await _dbContext.Users.FirstOrDefaultAsync(u => u.DisplayName.ToLower() == registerUserDTO.DisplayName.ToLower());
        if (existingDisplayName != null)
        {
            return Conflict("User with this display name already exists.");
        }

        var newUser = new User
        {
            FirstName = registerUserDTO.FirstName,
            LastName = registerUserDTO.LastName,
            DisplayName = registerUserDTO.DisplayName,
            Email = registerUserDTO.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDTO.Password),
            Roles = new[] { UserRole.User },
        };

        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync();

        var userResponse = new UserResponseDTO
        {
            Id = newUser.Id,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            DisplayName = newUser.DisplayName,
            Email = newUser.Email,
            CreatedAt = newUser.CreatedAt,
            CanSubmitRecipes = newUser.CanSubmitRecipes,
            Roles = newUser.Roles
        };

        return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, userResponse);
    }

    // ADMIN: list users (optional search by q over displayName, email, first/last)
    [HttpGet]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<ActionResult<PagedResult<AdminUserListItemDTO>>> GetAll(
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        page = page < 1 ? 1 : page;
        pageSize = pageSize is < 1 or > 100 ? 20 : pageSize;

        var query = _dbContext.Users.AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
        {
            var s = q.Trim().ToLower();
            query = query.Where(u =>
                u.DisplayName.ToLower().Contains(s) ||
                u.Email.ToLower().Contains(s) ||
                u.FirstName.ToLower().Contains(s) ||
                u.LastName.ToLower().Contains(s)
            );
        }

        var total = await query.CountAsync();
        var skip = (page - 1) * pageSize;

        var items = await query
            .OrderBy(u => u.DisplayName)
            .Skip(skip)
            .Take(pageSize)
            .Select(u => new AdminUserListItemDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                DisplayName = u.DisplayName,
                Email = u.Email,
                CreatedAt = u.CreatedAt,
                CanSubmitRecipes = u.CanSubmitRecipes,
                Roles = u.Roles,
                RecipesCount = _dbContext.Recipes.Count(r => r.UserId == u.Id),
                FavoritesReceived = _dbContext.Recipes.Where(r => r.UserId == u.Id).Sum(r => (int?)r.FavoritesCount) ?? 0
            })
            .ToListAsync();

        var result = new PagedResult<AdminUserListItemDTO>
        {
            Page = page,
            PageSize = pageSize,
            Total = total,
            Items = items
        };
        return Ok(result);
    }

    // ADMIN: update roles
    [HttpPut("{id}/roles")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<ActionResult<UserResponseDTO>> UpdateRoles(int id, [FromBody] UpdateUserRolesDTO dto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return NotFound("User not found.");

        // Normalize roles: ensure at least User
        var roles = (dto.Roles ?? Array.Empty<UserRole>()).Distinct().ToList();
        if (!roles.Contains(UserRole.User)) roles.Add(UserRole.User);

        // Guard: don't remove last Admin
        bool removingAdmin = user.Roles.Contains(UserRole.Admin) && !roles.Contains(UserRole.Admin);
        if (removingAdmin)
        {
            var adminCount = await _dbContext.Users.CountAsync(u => u.Roles.Contains(UserRole.Admin));
            if (adminCount <= 1)
            {
                return Conflict("Cannot remove the last admin.");
            }
        }

        user.Roles = roles.ToArray();
        user.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();

        var response = new UserResponseDTO
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DisplayName = user.DisplayName,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            CanSubmitRecipes = user.CanSubmitRecipes,
            Roles = user.Roles
        };
        return Ok(response);
    }

    // ADMIN: update permissions (currently only CanSubmitRecipes)
    [HttpPut("{id}/permissions")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<ActionResult<UserResponseDTO>> UpdatePermissions(int id, [FromBody] UpdateUserPermissionsDTO dto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return NotFound("User not found.");

        if (dto.CanSubmitRecipes.HasValue)
        {
            user.CanSubmitRecipes = dto.CanSubmitRecipes.Value;
        }
        user.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();

        var response = new UserResponseDTO
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DisplayName = user.DisplayName,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            CanSubmitRecipes = user.CanSubmitRecipes,
            Roles = user.Roles
        };
        return Ok(response);
    }

    // ADMIN: delete user (safe: forbid deleting self and ensure not the last admin)
    [HttpDelete("{id}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var jwt = Request.Cookies["jwt"];
        if (string.IsNullOrWhiteSpace(jwt)) return Unauthorized();
        var principal = _jwtService.ValidateToken(jwt);
        if (principal == null) return Unauthorized();
        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out var currentUserId)) return Unauthorized();

        if (currentUserId == id) return Conflict("You cannot delete your own account.");

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return NotFound("User not found.");

        if (user.Roles.Contains(UserRole.Admin))
        {
            var adminCount = await _dbContext.Users.CountAsync(u => u.Roles.Contains(UserRole.Admin));
            if (adminCount <= 1)
            {
                return Conflict("Cannot delete the last admin.");
            }
        }

        // Optional cascade considerations: recipes will remain with foreign key; depending on business rules you might transfer ownership or delete.
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return Unauthorized();
        }

        var userResponse = new UserResponseDTO
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DisplayName = user.DisplayName,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            CanSubmitRecipes = user.CanSubmitRecipes,
            Roles = user.Roles
        };

        return Ok(userResponse);
    }

    [HttpGet("{id}/recipes")]
    public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipesByUserId(int id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var userRecipes = await _dbContext.Recipes
            .Include(r => r.Category)
            .Where(recipe => recipe.UserId == id)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return Ok(userRecipes);
    }

    [HttpGet("{id}/favorites")]
    public async Task<ActionResult<IEnumerable<Recipe>>> GetFavoritesByUserId(int id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var favoriteRecipeIds = await _dbContext.RecipeFavorites
            .Where(rf => rf.UserId == id)
            .Select(rf => rf.RecipeId)
            .ToListAsync();

        var recipes = await _dbContext.Recipes
            .Include(r => r.Category)
            .Include(r => r.User)
            .Where(r => favoriteRecipeIds.Contains(r.Id))
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return Ok(recipes);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginUserDTO.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginUserDTO.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid email or password.");
        }

        var token = _jwtService.GenerateToken(user);
        var crossSite = Environment.GetEnvironmentVariable("CROSS_SITE_COOKIES") == "true";
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = !_environment.IsDevelopment(),
            SameSite = crossSite ? SameSiteMode.None : (_environment.IsDevelopment() ? SameSiteMode.Lax : SameSiteMode.Strict),
            Expires = DateTimeOffset.UtcNow.AddHours(24)
        };

        Response.Cookies.Append("jwt", token, cookieOptions);

        var response = new UserResponseDTO
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DisplayName = user.DisplayName,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            CanSubmitRecipes = user.CanSubmitRecipes,
            Roles = user.Roles
        };

        return Ok(response);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");
        return Ok(new { message = "Logged out successfully" });
    }

    [HttpPut("me")]
    public async Task<IActionResult> UpdateMe([FromBody] UpdateUserDTO dto)
    {
        var jwt = Request.Cookies["jwt"];
        if (string.IsNullOrWhiteSpace(jwt)) return Unauthorized();
        var principal = _jwtService.ValidateToken(jwt);
        if (principal == null) return Unauthorized();
        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out var userId)) return Unauthorized();

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return Unauthorized();

        // basic updates; ensure unique constraints on email/displayName if needed
        if (!string.IsNullOrWhiteSpace(dto.DisplayName)) user.DisplayName = dto.DisplayName.Trim();
        if (!string.IsNullOrWhiteSpace(dto.FirstName)) user.FirstName = dto.FirstName.Trim();
        if (!string.IsNullOrWhiteSpace(dto.LastName)) user.LastName = dto.LastName.Trim();
        if (!string.IsNullOrWhiteSpace(dto.Email)) user.Email = dto.Email.Trim();
        user.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();

        var response = new UserResponseDTO
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DisplayName = user.DisplayName,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            CanSubmitRecipes = user.CanSubmitRecipes,
            Roles = user.Roles
        };

        return Ok(response);
    }

    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var jwt = Request.Cookies["jwt"];
        if (string.IsNullOrWhiteSpace(jwt))
        {
            return Unauthorized();
        }

        var principal = _jwtService.ValidateToken(jwt);
        if (principal == null)
        {
            return Unauthorized();
        }

        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return Unauthorized();
        }

        var response = new UserResponseDTO
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DisplayName = user.DisplayName,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            CanSubmitRecipes = user.CanSubmitRecipes,
            Roles = user.Roles
        };

        return Ok(response);
    }
}