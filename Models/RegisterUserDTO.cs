namespace Recipes.Models;
public class RegisterUserDTO
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string DisplayName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}