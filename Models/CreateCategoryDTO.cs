namespace Recipes.Models;

public class CreateCategoryDTO
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}