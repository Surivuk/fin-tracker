namespace FinTracker.Transaction.Repository;

public interface ITransactionRepository
{
    public Task<TransactionModel> GetTransaction(string id);
    public void Save(TransactionModel transaction);
    public void Delete(string id);
}