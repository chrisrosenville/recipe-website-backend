using System.Text.Json.Serialization;

namespace Recipes.Models;

public class User
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string DisplayName { get; set; }
    public required string Email { get; set; }
    [JsonIgnore]
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole[] Roles { get; set; } = new[] { UserRole.User };
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool CanSubmitRecipes { get; set; } = true;
    [JsonIgnore]
    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}