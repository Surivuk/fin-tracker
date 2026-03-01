using FinTracker.Domain.Transaction.Abstractions;
using FinTracker.Domain.Transaction.Model;
using FinTracker.Persistence.Context;

namespace FinTracker.Persistence.Repository.Transaction;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    private readonly Repository<Category, CategoryId> _repo = context.TransactionCategories;

    public Task<Category> GetCategory(CategoryId id) => _repo.Find(id);
    public void Save(Category category) => _repo.Save(category);
    public void Delete(CategoryId id) => _repo.Delete(id);
}