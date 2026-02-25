using FinTracker.Domain.Transaction.Model;

namespace FinTracker.Domain.Transaction.Abstractions;

public interface ICategoryRepository
{
    public Task<Category> GetCategory(CategoryId id);
    public void Save(Category category);
    public void Delete(CategoryId id);
}