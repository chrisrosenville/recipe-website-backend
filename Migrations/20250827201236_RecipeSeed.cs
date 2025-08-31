using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recipes.Migrations
{
    /// <inheritdoc />
    public partial class RecipeSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CategoryId", "CookingTimeHours", "CookingTimeMinutes", "CreatedAt", "CurrentStatus", "Description", "FavoritesCount", "Image", "Ingredients", "Instructions", "Name", "Tags", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 0, 20, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Fluffy pancakes perfect for breakfast.", 0, "", new[] { "2 cups flour", "2 eggs", "1.5 cups milk", "2 tbsp sugar", "1 tsp baking powder", "Pinch of salt" }, new[] { "Mix dry ingredients.", "Add eggs and milk.", "Whisk until smooth.", "Cook on griddle until golden." }, "Classic Pancakes", new[] { "breakfast", "pancakes" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 2, 2, 0, 25, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "A healthy lunch salad with grilled chicken.", 0, "", new[] { "2 chicken breasts", "Romaine lettuce", "Caesar dressing", "Croutons", "Parmesan cheese" }, new[] { "Grill chicken.", "Chop lettuce.", "Mix all ingredients.", "Top with dressing and cheese." }, "Chicken Caesar Salad", new[] { "lunch", "salad" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 3, 3, 0, 40, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Classic Italian pasta dish with rich meat sauce.", 0, "", new[] { "200g spaghetti", "100g ground beef", "1 can tomato sauce", "1 onion", "2 cloves garlic", "Olive oil", "Salt", "Pepper" }, new[] { "Cook spaghetti.", "Sauté onion and garlic.", "Add beef and cook until brown.", "Pour in tomato sauce and simmer.", "Serve sauce over spaghetti." }, "Spaghetti Bolognese", new[] { "dinner", "pasta" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 4, 4, 0, 30, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Chewy cookies loaded with chocolate chips.", 0, "", new[] { "1 cup butter", "1 cup sugar", "2 cups flour", "2 eggs", "1 tsp vanilla", "1 tsp baking soda", "2 cups chocolate chips" }, new[] { "Preheat oven to 350°F (175°C).", "Cream butter and sugar.", "Add eggs and vanilla.", "Mix in flour and baking soda.", "Fold in chocolate chips.", "Drop by spoonfuls onto baking sheet.", "Bake for 10-12 minutes." }, "Chocolate Chip Cookies", new[] { "snack", "cookies" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 5, 2, 0, 15, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Fresh salad with tomatoes, mozzarella, and basil.", 0, "", new[] { "4 tomatoes", "200g mozzarella", "Fresh basil", "Olive oil", "Salt", "Pepper" }, new[] { "Slice tomatoes and mozzarella.", "Layer with basil leaves.", "Drizzle with olive oil.", "Season with salt and pepper." }, "Caprese Salad", new[] { "lunch", "salad" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 6, 3, 0, 20, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Quick and healthy vegetable stir-fry.", 0, "", new[] { "Mixed vegetables (bell peppers, broccoli, carrots)", "2 tbsp soy sauce", "1 tbsp olive oil", "2 cloves garlic", "1 tsp ginger" }, new[] { "Heat oil in a pan.", "Add garlic and ginger, sauté for 1 minute.", "Add vegetables and stir-fry until tender.", "Add soy sauce and cook for another 2 minutes." }, "Vegetable Stir-Fry", new[] { "dinner", "vegetarian" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
