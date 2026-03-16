using FinTracker.Transaction.Gateway;
using Microsoft.EntityFrameworkCore;

internal class CategoryOwnership(AppDbContext context) : ICategoryOwnership
{
    private readonly IQueryable<CategoryModel> categories = context.TransactionSchema.CategoryQuery;

    public async Task<bool> IsMyCategory(string userId, string categoryId) => (await categories
        .Where(c => c.Id == categoryId)
        .Where(c => c.UserId == userId)
        .ToListAsync()).Count != 0;

}