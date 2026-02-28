using FinTracker.Domain.Transaction.Abstractions;
using FinTracker.Domain.Transaction.Model;
using FinTracker.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinTracker.Persistence.Repository.Transaction;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    private readonly DbSet<Category> _set = context.TransactionCategories;

    public async Task<Category> GetCategory(CategoryId id)
    {
        var category = await _set.FirstAsync(t => t.Id == id) ?? throw new Exception($"Category not found! Id: \"{id}\"");
        return category;
    }

    public void Save(Category category)
    {
        var entry = context.Entry(category);

        if (entry.State == EntityState.Detached) _set.Add(category);
    }

    public void Delete(CategoryId id)
    {
        var category = _set.Local.FirstOrDefault(t => t.Id == id);

        category ??= CreateStub(id);

        _set.Remove(category);
    }

    private static Category CreateStub(CategoryId id) => new(id, UserId.From(Guid.NewGuid().ToString()));
}



