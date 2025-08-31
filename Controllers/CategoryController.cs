using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Recipes.Database;
using Recipes.Models;

namespace Recipes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(RecipeDbContext dbContext) : ControllerBase
{
    private readonly RecipeDbContext _dbContext = dbContext;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
    {
        var categories = await _dbContext.Categories
            .OrderBy(c => c.Name)
            .ToListAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _dbContext.Categories
            .Include(c => c.Recipes)  // Include recipes in this category
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Category>> CreateCategory([FromBody] CreateCategoryDTO newCategory)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null || !int.TryParse(userIdClaim, out int jwtUserId))
        {
            return Unauthorized();
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == jwtUserId);
        if (user == null || !user.Roles.Contains(UserRole.Admin))
        {
            return Unauthorized();
        }

        if (string.IsNullOrWhiteSpace(newCategory.Name))
        {
            return BadRequest("Category name is required.");
        }

        var existingCategory = await _dbContext.Categories
            .FirstOrDefaultAsync(c => c.Name.ToLower() == newCategory.Name.ToLower());

        if (existingCategory != null)
        {
            return Conflict("Category with this name already exists.");
        }

        var category = new Category
        {
            Name = newCategory.Name,
            Description = newCategory.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category updatedCategory)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null || !int.TryParse(userIdClaim, out int jwtUserId))
        {
            return Unauthorized();
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == jwtUserId);
        if (user == null || !user.Roles.Contains(UserRole.Admin))
        {
            return Forbid("Only administrators can update categories.");
        }

        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(updatedCategory.Name))
        {
            return BadRequest("Category name is required.");
        }

        var existingCategory = await _dbContext.Categories
            .FirstOrDefaultAsync(c => c.Name.ToLower() == updatedCategory.Name.ToLower() && c.Id != id);

        if (existingCategory != null)
        {
            return Conflict("Another category with this name already exists.");
        }

        category.Name = updatedCategory.Name;
        category.Description = updatedCategory.Description;
        category.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null || !int.TryParse(userIdClaim, out int jwtUserId))
        {
            return Unauthorized();
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == jwtUserId);
        if (user == null || !user.Roles.Contains(UserRole.Admin))
        {
            return Forbid("Only administrators can delete categories.");
        }

        var category = await _dbContext.Categories
            .Include(c => c.Recipes)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        if (category.Recipes.Any())
        {
            return Conflict("Cannot delete a category that has recipes. Move or delete its recipes first.");
        }

        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}