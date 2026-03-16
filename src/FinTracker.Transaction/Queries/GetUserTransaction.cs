using FinTracker.IDomain;
using FinTracker.Transaction.Gateway;

namespace FinTracker.Transaction.Queries;

public class GetUserTransaction(IUserQueries queries) : IDomainQuery<string, TransactionModel>
{
    public Task<TransactionModel> Execute(string transactionId) => queries.GetUserTransaction(transactionId);
}