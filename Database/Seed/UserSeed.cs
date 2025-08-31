using Recipes.Models;

namespace Recipes.Database.Seed;

public static class UserSeed
{

    private static readonly DateTime SeedDate = new(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public static readonly User[] Users =
    [
        new User
        {
            Id = 1,
            FirstName = "Seed",
            LastName = "User",
            DisplayName = "testuser",
            Email = "testuser@test.com",
            PasswordHash = "$2a$12$5TWK64v6F6PcHnH9d3b1QegvN3M218yWqbxwFJnROB6J7DenFONsC",
            CanSubmitRecipes = true,
            Roles = [UserRole.Author, UserRole.Admin],
            CreatedAt = SeedDate,
            UpdatedAt = SeedDate
        }
    ];
}