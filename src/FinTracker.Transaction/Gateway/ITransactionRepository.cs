namespace FinTracker.Transaction.Gateway;

public interface ITransactionRepository
{
    public Task<TransactionModel> GetTransaction(string id);
    public void Save(TransactionModel transaction);
    public void Delete(string id);
}