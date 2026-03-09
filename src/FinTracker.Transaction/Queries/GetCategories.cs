using FinTracker.IDomain;
using FinTracker.Transaction.Repository;

namespace FinTracker.Transaction.Queries;

public class GetCategories(ICategoryRepository repo) : IDomainQuery<IEnumerable<CategoryModel>>
{
    public Task<IEnumerable<CategoryModel>> Execute() => repo.GetCategories();
}