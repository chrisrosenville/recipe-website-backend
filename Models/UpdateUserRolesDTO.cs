using System.ComponentModel.DataAnnotations;

namespace Recipes.Models;

public class UpdateUserRolesDTO
{
    [Required]
    public required UserRole[] Roles { get; set; } = Array.Empty<UserRole>();
}
