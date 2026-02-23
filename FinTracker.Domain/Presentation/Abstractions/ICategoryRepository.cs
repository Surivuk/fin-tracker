using FinTracker.Domain.Presentation.Model;

namespace FinTracker.Domain.Presentation.Abstractions;

public interface ICategoryRepository
{
    public Task<Category> GetTransaction(string id);
    public void Save(Category category);
    public void Delete(string id);
}