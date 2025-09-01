using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Recipes.Database;
using Recipes.Models;

namespace Recipes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeController(RecipeDbContext dbContext) : ControllerBase
{
    private readonly RecipeDbContext _dbContext = dbContext;

    [HttpGet]
    public async Task<ActionResult<PagedResult<Recipe>>> GetAllRecipes([FromQuery] int page = 1, [FromQuery] int pageSize = 12)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 12;
        if (pageSize > 100) pageSize = 100; // Limit page size to prevent abuse

        var total = await _dbContext.Recipes.CountAsync();
        var recipes = await _dbContext.Recipes
            .Include(r => r.Category)
            .Include(r => r.User)
            .OrderByDescending(r => r.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var result = new PagedResult<Recipe>
        {
            Page = page,
            PageSize = pageSize,
            Total = total,
            Items = recipes
        };

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Recipe>> GetRecipeById(int id)

    {
        var foundRecipe = await _dbContext.Recipes
            .Include(r => r.Category)
            .Include(r => r.User)
            .FirstOrDefaultAsync(recipe => recipe.Id == id);

        if (foundRecipe == null)
        {
            return NotFound();
        }

        return Ok(foundRecipe);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] CreateRecipeDTO newRecipe)
    {
        if (newRecipe == null)
        {
            return BadRequest();
        }

        // JWT check
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null || !int.TryParse(userIdClaim, out var jwtUserId))
        {
            return Unauthorized("Invalid token");
        }

        // User check
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == jwtUserId);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        else if (!user.CanSubmitRecipes || !user.Roles.Contains(UserRole.Author))
        {
            return Forbid("User does not have permission to submit recipes.");
        }

        // Category check
        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == newRecipe.CategoryId);
        if (category == null)
        {
            return NotFound("Category not found.");
        }

        var createdRecipe = new Recipe
        {
            UserId = jwtUserId,
            Name = newRecipe.Name,
            Description = newRecipe.Description,
            CategoryId = category.Id,
            Category = category,
            Ingredients = newRecipe.Ingredients,
            Instructions = newRecipe.Instructions,
            Image = newRecipe.Image,
            Tags = newRecipe.Tags,
            CookingTimeHours = newRecipe.CookingTimeHours,
            CookingTimeMinutes = newRecipe.CookingTimeMinutes,
            FavoritesCount = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _dbContext.Recipes.Add(createdRecipe);
        await _dbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRecipeById), new { id = createdRecipe.Id }, createdRecipe);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Recipe>> UpdateRecipe(int id, [FromBody] Recipe updatedRecipe)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null || !int.TryParse(userIdClaim, out int jwtUserId))
        {
            return Unauthorized("Invalid token.");
        }

        var existingRecipe = await _dbContext.Recipes.FirstOrDefaultAsync(recipe => recipe.Id == id);
        if (existingRecipe == null)
        {
            return NotFound();
        }
        else if (existingRecipe.UserId != jwtUserId)
        {
            return Forbid("You do not have permission to edit this recipe.");
        }

        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == updatedRecipe.CategoryId);
        if (category == null)
        {
            return NotFound("Category not found.");
        }

        existingRecipe.Name = updatedRecipe.Name;
        existingRecipe.Description = updatedRecipe.Description;
        existingRecipe.CurrentStatus = updatedRecipe.CurrentStatus;
        existingRecipe.CategoryId = category.Id;
        existingRecipe.Category = category;
        existingRecipe.Ingredients = updatedRecipe.Ingredients;
        existingRecipe.Instructions = updatedRecipe.Instructions;
        existingRecipe.Image = updatedRecipe.Image;
        existingRecipe.Tags = updatedRecipe.Tags;
        existingRecipe.CookingTimeHours = updatedRecipe.CookingTimeHours;
        existingRecipe.CookingTimeMinutes = updatedRecipe.CookingTimeMinutes;
        existingRecipe.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();

        return Ok(existingRecipe);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> DeleteRecipe(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null || !int.TryParse(userIdClaim, out int jwtUserId))
        {
            return Unauthorized("Invalid token.");
        }

        var existingRecipe = await _dbContext.Recipes.FirstOrDefaultAsync(recipe => recipe.Id == id);

        if (existingRecipe == null)
        {
            return NotFound();
        }
        else if (existingRecipe.UserId != jwtUserId)
        {
            return Forbid("You do not have permission to delete this recipe.");
        }

        _dbContext.Recipes.Remove(existingRecipe);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    // Get favorites info for a recipe (count and whether current user favorited)
    [HttpGet("{id}/favorite")]
    public async Task<ActionResult<object>> GetFavoriteInfo(int id)
    {
        var count = await _dbContext.RecipeFavorites.CountAsync(rf => rf.RecipeId == id);
        bool isFavorited = false;
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim != null && int.TryParse(userIdClaim, out var uid))
        {
            isFavorited = await _dbContext.RecipeFavorites.AnyAsync(rf => rf.RecipeId == id && rf.UserId == uid);
        }
        return Ok(new { count, isFavorited });
    }

    // Favorites (naive counter). For a robust solution, add a join table to track user favorites.
    [HttpPost("{id}/favorite")]
    [Authorize]
    public async Task<ActionResult<int>> FavoriteRecipe(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null || !int.TryParse(userIdClaim, out int jwtUserId))
        {
            return Unauthorized("Invalid token.");
        }

        var recipe = await _dbContext.Recipes.FirstOrDefaultAsync(r => r.Id == id);
        if (recipe == null)
        {
            return NotFound();
        }

        var already = await _dbContext.RecipeFavorites
            .AnyAsync(rf => rf.UserId == jwtUserId && rf.RecipeId == id);
        if (!already)
        {
            _dbContext.RecipeFavorites.Add(new RecipeFavorite
            {
                UserId = jwtUserId,
                RecipeId = id,
            });
            await _dbContext.SaveChangesAsync();
        }

        recipe.FavoritesCount = await _dbContext.RecipeFavorites
            .CountAsync(rf => rf.RecipeId == id);
        await _dbContext.SaveChangesAsync();

        return Ok(recipe.FavoritesCount);
    }

    [HttpDelete("{id}/favorite")]
    [Authorize]
    public async Task<ActionResult<int>> UnfavoriteRecipe(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null || !int.TryParse(userIdClaim, out int jwtUserId))
        {
            return Unauthorized("Invalid token.");
        }

        var recipe = await _dbContext.Recipes.FirstOrDefaultAsync(r => r.Id == id);
        if (recipe == null)
        {
            return NotFound();
        }

        var fav = await _dbContext.RecipeFavorites
            .FirstOrDefaultAsync(rf => rf.UserId == jwtUserId && rf.RecipeId == id);
        if (fav != null)
        {
            _dbContext.RecipeFavorites.Remove(fav);
            await _dbContext.SaveChangesAsync();
        }

        recipe.FavoritesCount = await _dbContext.RecipeFavorites
            .CountAsync(rf => rf.RecipeId == id);
        await _dbContext.SaveChangesAsync();

        return Ok(recipe.FavoritesCount);
    }
}