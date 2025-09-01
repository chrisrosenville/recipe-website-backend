using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recipes.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreSeededRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CategoryId", "CookingTimeHours", "CookingTimeMinutes", "CreatedAt", "CurrentStatus", "Description", "FavoritesCount", "Image", "Ingredients", "Instructions", "Name", "Tags", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 7, 1, 0, 10, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Simple and nutritious breakfast toast with avocado.", 0, "", new[] { "2 slices whole-grain bread", "1 ripe avocado", "Salt", "Pepper", "Lemon juice", "Chili flakes (optional)", "Olive oil (optional)" }, new[] { "Toast the bread.", "Mash avocado with lemon juice, salt, and pepper.", "Spread on toast.", "Drizzle with olive oil and sprinkle chili flakes if using." }, "Avocado Toast", new[] { "breakfast", "quick" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 8, 1, 0, 12, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Fluffy omelette filled with cheese.", 0, "", new[] { "3 eggs", "2 tbsp milk", "1/2 cup shredded cheese", "1 tbsp butter", "Salt", "Pepper" }, new[] { "Whisk eggs, milk, salt, and pepper.", "Melt butter in a pan.", "Pour eggs and cook until almost set.", "Add cheese, fold, and cook until melted." }, "Cheese Omelette", new[] { "breakfast", "eggs" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 9, 1, 0, 15, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Classic cinnamon vanilla French toast.", 0, "", new[] { "4 slices bread", "2 eggs", "1/2 cup milk", "1 tsp vanilla", "1/2 tsp cinnamon", "Butter", "Maple syrup" }, new[] { "Whisk eggs, milk, vanilla, and cinnamon.", "Dip bread in mixture.", "Cook on buttered skillet until golden on both sides.", "Serve with maple syrup." }, "French Toast", new[] { "breakfast", "sweet" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 10, 1, 0, 8, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Thick smoothie bowl topped with fruit and granola.", 0, "", new[] { "1 cup frozen berries", "1 banana", "1/2 cup yogurt", "1/2 cup milk", "Granola", "Fresh fruit", "Chia seeds" }, new[] { "Blend berries, banana, yogurt, and milk until thick.", "Pour into a bowl.", "Top with granola, fresh fruit, and chia seeds." }, "Berry Smoothie Bowl", new[] { "breakfast", "smoothie" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 11, 1, 0, 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Layered yogurt parfait with granola and honey.", 0, "", new[] { "1 cup yogurt", "1/2 cup granola", "1/2 cup mixed berries", "1 tbsp honey" }, new[] { "Layer yogurt, granola, and berries in a glass.", "Drizzle honey on top.", "Serve immediately." }, "Granola Parfait", new[] { "breakfast", "no-cook" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 12, 2, 0, 15, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Hearty triple-decker turkey club.", 0, "", new[] { "3 slices bread", "Sliced turkey", "Bacon", "Lettuce", "Tomato", "Mayonnaise", "Butter" }, new[] { "Toast bread and spread with mayo.", "Layer turkey, bacon, lettuce, and tomato.", "Stack and slice in half." }, "Turkey Club Sandwich", new[] { "lunch", "sandwich" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 13, 2, 0, 20, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Protein-packed quinoa salad with fresh veggies.", 0, "", new[] { "1 cup cooked quinoa", "Cherry tomatoes", "Cucumber", "Red onion", "Feta cheese", "Olives", "Olive oil", "Lemon juice", "Salt", "Pepper" }, new[] { "Combine cooked quinoa and chopped vegetables.", "Crumble in feta and add olives.", "Dress with olive oil, lemon juice, salt, and pepper.", "Toss and serve." }, "Quinoa Veggie Salad", new[] { "lunch", "vegetarian" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 14, 2, 0, 25, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Comforting tomato soup with a touch of cream.", 0, "", new[] { "1 tbsp olive oil", "1 onion", "2 cloves garlic", "1 can crushed tomatoes", "1 cup vegetable broth", "1/4 cup cream", "Salt", "Pepper", "Basil" }, new[] { "Sauté onion and garlic in olive oil.", "Add tomatoes and broth; simmer 10 minutes.", "Blend until smooth.", "Stir in cream, season, and garnish with basil." }, "Creamy Tomato Soup", new[] { "lunch", "soup" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 15, 2, 0, 10, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Colorful veggie wrap with hummus.", 0, "", new[] { "Large tortilla", "Hummus", "Bell peppers", "Cucumber", "Spinach", "Carrots", "Avocado", "Salt", "Pepper" }, new[] { "Spread hummus on tortilla.", "Layer sliced veggies and avocado.", "Season, roll tightly, and slice in half." }, "Veggie Wrap", new[] { "lunch", "vegan" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 16, 2, 0, 10, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Classic tuna salad perfect for sandwiches or greens.", 0, "", new[] { "1 can tuna", "2 tbsp mayo", "1 tbsp mustard", "Celery", "Red onion", "Salt", "Pepper", "Lemon juice" }, new[] { "Drain tuna and combine with chopped celery and onion.", "Mix in mayo, mustard, and lemon juice.", "Season with salt and pepper.", "Serve as a sandwich or over greens." }, "Tuna Salad", new[] { "lunch", "seafood" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 17, 3, 0, 25, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Seasoned ground beef tacos with fresh toppings.", 0, "", new[] { "500g ground beef", "Taco seasoning", "Taco shells", "Lettuce", "Tomato", "Cheddar", "Sour cream", "Salsa" }, new[] { "Cook beef with taco seasoning.", "Warm taco shells.", "Assemble with toppings and serve." }, "Beef Tacos", new[] { "dinner", "mexican" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 18, 3, 0, 20, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Lemon garlic grilled salmon fillets.", 0, "", new[] { "2 salmon fillets", "2 tbsp olive oil", "2 cloves garlic", "Lemon", "Salt", "Pepper", "Fresh dill" }, new[] { "Brush salmon with oil, garlic, salt, and pepper.", "Grill 3-4 minutes per side.", "Serve with lemon wedges and dill." }, "Grilled Salmon", new[] { "dinner", "seafood" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 19, 3, 0, 35, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Creamy coconut chicken curry.", 0, "", new[] { "500g chicken", "1 onion", "2 cloves garlic", "1 tbsp curry powder", "1 tsp ginger", "1 can coconut milk", "Oil", "Salt" }, new[] { "Sauté onion, garlic, and ginger.", "Add chicken and brown.", "Stir in curry powder.", "Pour coconut milk and simmer until cooked." }, "Chicken Curry", new[] { "dinner", "curry" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 20, 3, 0, 50, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Layers of pasta with veggies and cheese.", 0, "", new[] { "Lasagna noodles", "Marinara sauce", "Zucchini", "Spinach", "Ricotta", "Mozzarella", "Parmesan", "Olive oil", "Salt", "Pepper" }, new[] { "Sauté zucchini and spinach.", "Layer noodles, sauce, veggies, and cheeses.", "Bake at 375°F (190°C) for 30-35 minutes." }, "Vegetable Lasagna", new[] { "dinner", "vegetarian" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 21, 3, 0, 20, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Quick fried rice with shrimp and vegetables.", 0, "", new[] { "2 cups cooked rice", "200g shrimp", "Frozen peas and carrots", "2 eggs", "Soy sauce", "Sesame oil", "Green onions", "Garlic" }, new[] { "Scramble eggs and set aside.", "Stir-fry garlic, shrimp, and veggies.", "Add rice, soy sauce, and sesame oil.", "Stir in eggs and green onions." }, "Shrimp Fried Rice", new[] { "dinner", "stir-fry" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 22, 3, 0, 25, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Tender beef with vegetables in a savory sauce.", 0, "", new[] { "300g beef strips", "Broccoli", "Bell peppers", "Soy sauce", "Garlic", "Ginger", "Cornstarch", "Vegetable oil" }, new[] { "Marinate beef in soy sauce and cornstarch.", "Stir-fry beef until browned; remove.", "Cook veggies with garlic and ginger.", "Return beef, add sauce, and toss." }, "Beef Stir-Fry", new[] { "dinner", "asian" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 23, 3, 0, 20, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Classic pizza with tomato, mozzarella, and basil.", 0, "", new[] { "Pizza dough", "Tomato sauce", "Fresh mozzarella", "Fresh basil", "Olive oil", "Salt" }, new[] { "Preheat oven to highest setting.", "Stretch dough and spread sauce.", "Top with mozzarella and bake until bubbly.", "Garnish with basil and olive oil." }, "Margherita Pizza", new[] { "dinner", "pizza" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 24, 3, 0, 45, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Hearty lentil stew with vegetables.", 0, "", new[] { "1 cup lentils", "1 onion", "2 carrots", "2 celery stalks", "2 cloves garlic", "1 can diced tomatoes", "Vegetable broth", "Bay leaf", "Salt", "Pepper" }, new[] { "Sauté onion, carrots, and celery.", "Add garlic, lentils, tomatoes, broth, and bay leaf.", "Simmer until lentils are tender.", "Season and serve." }, "Lentil Stew", new[] { "dinner", "vegan" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 25, 3, 0, 40, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Bell peppers filled with rice and beef.", 0, "", new[] { "4 bell peppers", "1 cup cooked rice", "300g ground beef", "1 onion", "Tomato sauce", "Cheddar", "Salt", "Pepper" }, new[] { "Sauté beef and onion; mix with rice and sauce.", "Stuff peppers and top with cheese.", "Bake at 375°F (190°C) for 25-30 minutes." }, "Stuffed Bell Peppers", new[] { "dinner", "bake" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 26, 4, 0, 10, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Creamy avocado dip with lime and cilantro.", 0, "", new[] { "2 ripe avocados", "1/2 onion", "1 tomato", "1 lime", "Cilantro", "Salt", "Pepper" }, new[] { "Mash avocados.", "Stir in finely chopped onion, tomato, and cilantro.", "Add lime juice, salt, and pepper to taste." }, "Guacamole", new[] { "snack", "dip" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 27, 4, 0, 10, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Smooth chickpea hummus with tahini.", 0, "", new[] { "1 can chickpeas", "2 tbsp tahini", "1 clove garlic", "2 tbsp olive oil", "Lemon juice", "Salt", "Paprika" }, new[] { "Blend chickpeas, tahini, garlic, lemon juice, and salt.", "Stream in olive oil until smooth.", "Garnish with paprika." }, "Classic Hummus", new[] { "snack", "vegan" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 28, 4, 0, 15, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Toasted bread topped with tomato and basil.", 0, "", new[] { "Baguette", "Tomatoes", "Garlic", "Basil", "Olive oil", "Salt", "Pepper", "Balsamic glaze (optional)" }, new[] { "Toast sliced baguette.", "Mix chopped tomatoes with garlic, basil, oil, salt, and pepper.", "Spoon onto toast and drizzle glaze if desired." }, "Bruschetta", new[] { "snack", "appetizer" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 29, 4, 1, 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Moist banana bread loaf with walnuts.", 0, "", new[] { "3 ripe bananas", "1/3 cup melted butter", "3/4 cup sugar", "1 egg", "1 tsp vanilla", "1 tsp baking soda", "Pinch of salt", "1.5 cups flour", "1/2 cup walnuts" }, new[] { "Mash bananas and mix with butter and sugar.", "Beat in egg and vanilla.", "Stir in baking soda, salt, and flour.", "Fold in walnuts.", "Bake in loaf pan at 350°F (175°C) for 50-60 minutes." }, "Banana Bread", new[] { "snack", "bake" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 30, 4, 0, 30, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Fudgy chocolate brownies with crackly top.", 0, "", new[] { "1/2 cup butter", "1 cup sugar", "2 eggs", "1 tsp vanilla", "1/3 cup cocoa powder", "1/2 cup flour", "1/4 tsp salt", "1/4 tsp baking powder" }, new[] { "Melt butter and mix with sugar.", "Beat in eggs and vanilla.", "Stir in dry ingredients.", "Bake at 350°F (175°C) for 20-25 minutes." }, "Chocolate Brownies", new[] { "snack", "chocolate" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 31, 1, 0, 20, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Eggs, potatoes, and cheese wrapped in a warm tortilla.", 0, "", new[] { "Large tortilla", "2 eggs", "1/2 cup diced potatoes", "1/4 cup shredded cheese", "Salsa", "Salt", "Pepper", "Oil" }, new[] { "Cook diced potatoes in oil until tender.", "Scramble eggs and season.", "Warm tortilla and fill with potatoes, eggs, cheese, and salsa.", "Roll up and serve." }, "Breakfast Burrito", new[] { "breakfast", "wrap" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 32, 1, 0, 40, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Warm baked oats with berries and cinnamon.", 0, "", new[] { "2 cups rolled oats", "1 tsp baking powder", "1 tsp cinnamon", "Pinch of salt", "2 cups milk", "1 egg", "2 tbsp maple syrup", "1 cup mixed berries" }, new[] { "Preheat oven to 350°F (175°C).", "Mix dry ingredients.", "Whisk milk, egg, and maple syrup.", "Combine with oats and fold in berries.", "Bake 30–35 minutes until set." }, "Baked Oatmeal", new[] { "breakfast", "oats" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 33, 1, 0, 30, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Eggs poached in a spiced tomato pepper sauce.", 0, "", new[] { "2 tbsp olive oil", "1 onion", "1 bell pepper", "2 cloves garlic", "1 tsp cumin", "1 tsp paprika", "1 can crushed tomatoes", "4 eggs", "Salt", "Pepper", "Parsley" }, new[] { "Sauté onion and pepper in oil.", "Add garlic and spices.", "Pour in tomatoes and simmer.", "Make wells and crack eggs; cover until set.", "Season and garnish with parsley." }, "Shakshuka", new[] { "breakfast", "eggs" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 34, 1, 0, 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "No-cook oats soaked overnight with chia and fruit.", 0, "", new[] { "1/2 cup rolled oats", "1 tbsp chia seeds", "1/2 cup milk", "1/4 cup yogurt", "1 tsp honey", "Fresh fruit" }, new[] { "Combine oats, chia, milk, yogurt, and honey in a jar.", "Stir, cover, and refrigerate overnight.", "Top with fruit before serving." }, "Overnight Oats", new[] { "breakfast", "no-cook" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 35, 2, 0, 15, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Crisp cucumber, tomato, olives, and feta with oregano.", 0, "", new[] { "Cucumber", "Tomatoes", "Red onion", "Kalamata olives", "Feta", "Olive oil", "Red wine vinegar", "Oregano", "Salt", "Pepper" }, new[] { "Chop vegetables and combine with olives and feta.", "Dress with oil, vinegar, oregano, salt, and pepper.", "Toss and serve." }, "Greek Salad", new[] { "lunch", "salad" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 36, 2, 0, 12, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Classic bacon, lettuce, and tomato on toasted bread.", 0, "", new[] { "2 slices bread", "Bacon", "Lettuce", "Tomato", "Mayonnaise", "Salt", "Pepper" }, new[] { "Cook bacon until crisp.", "Toast bread and spread with mayo.", "Layer bacon, lettuce, and tomato; season and serve." }, "BLT Sandwich", new[] { "lunch", "sandwich" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 37, 2, 0, 35, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Hearty Italian vegetable and bean soup.", 0, "", new[] { "Olive oil", "Onion", "Carrot", "Celery", "Garlic", "Diced tomatoes", "Vegetable broth", "Cannellini beans", "Small pasta", "Zucchini", "Spinach", "Salt", "Pepper" }, new[] { "Sauté onion, carrot, celery, and garlic.", "Add tomatoes and broth; simmer.", "Stir in beans and pasta; cook until tender.", "Add zucchini and spinach; season and serve." }, "Minestrone Soup", new[] { "lunch", "soup" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 38, 4, 0, 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Customizable nuts, seeds, dried fruit, and chocolate.", 0, "", new[] { "Almonds", "Cashews", "Pumpkin seeds", "Raisins", "Dried cranberries", "Dark chocolate chips", "Sea salt" }, new[] { "Combine all ingredients in a bowl.", "Toss to mix and store in an airtight container." }, "Trail Mix", new[] { "snack", "no-cook" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 39, 4, 0, 10, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Fresh seasonal fruit with a honey-lime dressing.", 0, "", new[] { "Strawberries", "Blueberries", "Pineapple", "Grapes", "Kiwi", "Honey", "Lime juice", "Mint" }, new[] { "Chop fruit and combine in a bowl.", "Whisk honey with lime juice.", "Pour over fruit and toss with chopped mint." }, "Fruit Salad", new[] { "snack", "fruit" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 40, 4, 0, 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Simple snack board with assorted cheeses and crackers.", 0, "", new[] { "Assorted crackers", "Cheddar", "Brie", "Gouda", "Grapes", "Honey" }, new[] { "Arrange cheeses and crackers on a board.", "Add grapes and a drizzle of honey." }, "Cheese and Crackers", new[] { "snack", "board" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 41, 4, 0, 7, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Greek yogurt ranch dip served with crunchy vegetables.", 0, "", new[] { "1 cup Greek yogurt", "1 tsp dried dill", "1/2 tsp garlic powder", "1/2 tsp onion powder", "1 tsp lemon juice", "Salt", "Pepper", "Assorted veggie sticks" }, new[] { "Mix yogurt with dill, garlic and onion powder, and lemon juice.", "Season with salt and pepper.", "Serve with veggie sticks." }, "Yogurt Ranch Dip with Veggies", new[] { "snack", "dip" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 42, 1, 0, 15, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Crispy tortilla filled with scrambled eggs, cheese, and veggies.", 0, "", new[] { "Large tortilla", "2 eggs", "1/3 cup shredded cheese", "Bell peppers", "Spinach", "Butter or oil", "Salt", "Pepper", "Salsa (optional)" }, new[] { "Scramble eggs and season.", "Warm tortilla in a pan; add cheese, eggs, and veggies.", "Fold and cook until golden on both sides.", "Serve with salsa if desired." }, "Breakfast Quesadilla", new[] { "breakfast", "wrap" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 43, 2, 0, 15, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Protein-packed bowl with chickpeas, veggies, and lemon-tahini dressing.", 0, "", new[] { "1 can chickpeas", "Cherry tomatoes", "Cucumber", "Red onion", "Kalamata olives", "Parsley", "Feta (optional)", "Olive oil", "Lemon juice", "Tahini", "Salt", "Pepper" }, new[] { "Rinse and drain chickpeas.", "Chop vegetables and parsley.", "Whisk olive oil, lemon juice, tahini, salt, and pepper.", "Combine all and toss with dressing; top with feta if using." }, "Mediterranean Chickpea Bowl", new[] { "lunch", "vegetarian" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 44, 4, 0, 15, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "No-bake oats, peanut butter, and honey bites.", 0, "", new[] { "1 cup rolled oats", "1/2 cup peanut butter", "1/3 cup honey", "2 tbsp chia seeds", "2 tbsp flaxseed meal", "Mini chocolate chips (optional)" }, new[] { "Stir all ingredients together until combined.", "Chill 15 minutes.", "Roll into bite-sized balls and refrigerate." }, "Peanut Butter Energy Bites", new[] { "snack", "no-bake" }, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 44);
        }
    }
}
