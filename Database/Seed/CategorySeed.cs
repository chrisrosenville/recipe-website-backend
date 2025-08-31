using Recipes.Models;

namespace Recipes.Database.SeedData;

public static class CategorySeed
{
    private static readonly DateTime SeedDate = new(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public static readonly Category[] Categories =
    [
        new Category { Id = 1, Name = "Breakfast", Description = "Start your day with energy", CreatedAt = SeedDate, UpdatedAt = SeedDate },
        new Category { Id = 2, Name = "Lunch", Description = "Refuel your body with a healthy lunch", CreatedAt = SeedDate, UpdatedAt = SeedDate },
        new Category { Id = 3, Name = "Dinner", Description = "End your day with a delicious dinner", CreatedAt = SeedDate, UpdatedAt = SeedDate },
        new Category { Id = 4, Name = "Snack", Description = "Quick and easy snacks for any time", CreatedAt = SeedDate, UpdatedAt = SeedDate }
    ];

}