using FinTracker.Domain.Presentation.Abstractions;
using FinTracker.Domain.Presentation.Model;
using FinTracker.Persistence.Context;

namespace FinTracker.Persistence.Repository.Presentation;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    private readonly Repository<Category, EntityId> _repo = context.PresentationCategories;

    public Task<Category> GetCategory(EntityId id) => _repo.Find(id);

    public void Save(Category category) => _repo.Save(category);

    public void Delete(EntityId id) => _repo.Delete(id);
}



