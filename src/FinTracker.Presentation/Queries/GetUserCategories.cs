using FinTracker.IDomain;
using FinTracker.Presentation.Gateway;

namespace FinTracker.Presentation.Queries;

public class GetUserCategories(IUserQueries queries) : IDomainQuery<IEnumerable<UserCategoryModel>>
{
    public Task<IEnumerable<UserCategoryModel>> Execute() => queries.GetUserCategories();
}