namespace Recipes.Models;

public class UserResponseDTO
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string DisplayName { get; set; }
    public required string Email { get; set; }
    public required bool CanSubmitRecipes { get; set; }
    public required UserRole[] Roles { get; set; }
    public DateTime CreatedAt { get; set; }
}