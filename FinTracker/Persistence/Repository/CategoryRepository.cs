using FinTracker.Domain.Transaction.Abstractions;
using FinTracker.Domain.Transaction.Model;
using FinTracker.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinTracker.Persistence.Repository;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    private readonly DbSet<Category> _set = context.Categories;

    public async Task<Category> GetCategory(CategoryId id)
    {
        var category = await _set.FirstAsync(t => t.Id == id);

        if (category is null) throw new Exception($"Not found category! Id: \"{id}\"");

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

    private Category CreateStub(CategoryId id)
    {
        var entry = _set.Entry((Category)Activator.CreateInstance(typeof(Category), nonPublic: true)!);

        entry.Property(t => t.Id).CurrentValue = id;
        entry.State = EntityState.Deleted;

        return entry.Entity;
    }
}



