namespace Recipes.Models;    
    public class Recipe
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public required string Name { get; set; }
        public required string Description { get; set; }
        public RecipeStatus CurrentStatus { get; set; } = RecipeStatus.Draft;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public required string[] Ingredients { get; set; } = Array.Empty<string>();
        public required string[] Instructions { get; set; } = Array.Empty<string>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string Image { get; set; } = "";
        public string[] Tags { get; set; } = Array.Empty<string>();
        public int FavoritesCount { get; set; } = 0;

        public int CookingTimeHours { get; set; }
        public int CookingTimeMinutes { get; set; }
    }