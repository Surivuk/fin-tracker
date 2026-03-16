using FinTracker.IDomain;
using FinTracker.Transaction.Gateway;

namespace FinTracker.Transaction.Queries;

public class GetUserTransactions(IUserQueries queries) : IDomainQuery<IEnumerable<TransactionModel>>
{
    public Task<IEnumerable<TransactionModel>> Execute() => queries.GetUserTransactions();
}