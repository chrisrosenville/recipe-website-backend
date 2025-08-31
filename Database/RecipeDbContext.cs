using Microsoft.EntityFrameworkCore;
using Recipes.Database.Seed;
using Recipes.Database.SeedData;
using Recipes.Models;

namespace Recipes.Database;

public class RecipeDbContext(DbContextOptions<RecipeDbContext> options) : DbContext(options)
{
    public DbSet<Recipe> Recipes { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<RecipeFavorite> RecipeFavorites { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.User)
            .WithMany(u => u.Recipes)
            .HasForeignKey(r => r.UserId);

        // Category-Recipe relationship
        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.Category)
            .WithMany(c => c.Recipes)
            .HasForeignKey(r => r.CategoryId);

        // Favorites (many-to-many via explicit join entity)
        modelBuilder.Entity<RecipeFavorite>()
            .HasKey(rf => new { rf.UserId, rf.RecipeId });

        modelBuilder.Entity<RecipeFavorite>()
            .HasOne(rf => rf.User)
            .WithMany()
            .HasForeignKey(rf => rf.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RecipeFavorite>()
            .HasOne(rf => rf.Recipe)
            .WithMany()
            .HasForeignKey(rf => rf.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>().HasData(UserSeed.Users);
        modelBuilder.Entity<Category>().HasData(CategorySeed.Categories);
        modelBuilder.Entity<Recipe>().HasData(RecipeSeed.Recipes);
    }
}