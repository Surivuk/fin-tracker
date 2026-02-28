using FinTracker.Domain.Presentation.Abstractions;
using FinTracker.Domain.Presentation.Model;
using FinTracker.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinTracker.Persistence.Repository.Presentation;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    private readonly DbSet<Category> _set = context.PresentationCategories;

    public async Task<Category> GetCategory(EntityId id)
    {
        var category = await _set.FirstAsync(t => t.Id == id) ?? throw new Exception($"Category not found! Id: \"{id}\"");
        return category;
    }

    public void Save(Category category)
    {
        var entry = context.Entry(category);

        if (entry.State == EntityState.Detached) _set.Add(category);
    }

    public void Delete(EntityId id)
    {
        var category = _set.Local.FirstOrDefault(t => t.Id == id);

        category ??= CreateStub(id);

        _set.Remove(category);
    }

    private static Category CreateStub(EntityId id) => new(id, default, EntityInformation.Default, CategoryAppearance.Default);
}



