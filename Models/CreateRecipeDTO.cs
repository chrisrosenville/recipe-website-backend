namespace Recipes.Models;

public class CreateRecipeDTO
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int CategoryId { get; set; }
    public required string[] Ingredients { get; set; } = Array.Empty<string>();
    public required string[] Instructions { get; set; } = Array.Empty<string>();
    public string Image { get; set; } = string.Empty;
    public string[] Tags { get; set; } = Array.Empty<string>();
    public int CookingTimeHours { get; set; }
    public int CookingTimeMinutes { get; set; }
}