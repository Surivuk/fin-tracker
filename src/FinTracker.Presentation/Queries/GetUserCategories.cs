using FinTracker.IDomain;
using FinTracker.Presentation.Gateway;

namespace FinTracker.Presentation.Queries;

public class GetUserCategories(IUserQueries queries) : IDomainQuery<IReadOnlyList<UserCategoryModel>>
{
    public Task<IReadOnlyList<UserCategoryModel>> Execute() => queries.GetUserCategories();
}