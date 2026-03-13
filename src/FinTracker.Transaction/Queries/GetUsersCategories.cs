using FinTracker.IDomain;
using FinTracker.Transaction.Repository;

namespace FinTracker.Transaction.Queries;

public class GetUsersCategories(IUserQueries queries) : IDomainQuery<IEnumerable<string>>
{
    public Task<IEnumerable<string>> Execute() => queries.GetUserCategories();
}