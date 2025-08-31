namespace Recipes.Models;

public class PagedResult<T>
{
    public required int Page { get; set; }
    public required int PageSize { get; set; }
    public required int Total { get; set; }
    public required IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();
}
