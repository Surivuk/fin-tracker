using FinTracker.Identity.Gateway;
using FinTracker.IDomain;

namespace FinTracker.Identity.Queries;

public class GetUser(IUserQueries query) : IDomainQuery<UserData>
{
    public Task<UserData> Execute() => query.GetUser();
}