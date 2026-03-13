using FinTracker.Presentation.Gateway;
using Microsoft.EntityFrameworkCore;

internal class PresentationUserQueries(AppDbContext context, string UserId) : IUserQueries
{
    private readonly IQueryable<CategoryModel> query = context.PresentationSchema.CategoryQuery;

    public async Task<IReadOnlyList<UserCategoryModel>> GetUserCategories() => await query
        .Where(e => e.Id == UserId)
        .Select(e => new UserCategoryModel(e.Id, e.Title, e.Description, e.Color))
        .ToListAsync();

    public async Task<UserCategoryModel> GetUserCategory(string categoryId) => await query
        .Where(e => e.Id == UserId)
        .Select(e => new UserCategoryModel(e.Id, e.Title, e.Description, e.Color))
        .FirstAsync() ?? throw new Exception($"Not found category with id:${categoryId}");
}