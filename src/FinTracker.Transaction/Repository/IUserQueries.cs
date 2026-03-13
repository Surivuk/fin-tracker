namespace FinTracker.Transaction.Repository;

public interface IUserQueries
{
    public Task<IEnumerable<string>> GetUserCategories();

    public Task<IEnumerable<TransactionModel>> GetUserTransactions();

    public Task<TransactionModel> GetUserTransaction(string transactionId);
}