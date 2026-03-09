using FinTracker.IDomain;
using FinTracker.Transaction.Repository;

namespace FinTracker.Transaction.Queries;

public class GetCategory(ICategoryRepository repo) : IDomainQuery<string, CategoryModel>
{
    public Task<CategoryModel> Execute(string request) => repo.GetCategory(request);
}