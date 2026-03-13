using FinTracker.IDomain;
using FinTracker.Presentation.Gateway;

namespace FinTracker.Presentation.Queries;

public class GetUserCategory(IUserQueries queries) : IDomainQuery<string, UserCategoryModel>
{
    public Task<UserCategoryModel> Execute(string categoryId) => queries.GetUserCategory(categoryId);
}