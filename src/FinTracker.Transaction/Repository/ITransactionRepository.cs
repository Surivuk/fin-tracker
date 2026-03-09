namespace FinTracker.Transaction.Repository;

public record TransactionModel(string Id, string CategoryId, double MoneyAmount, string MoneyCurrency, string Type);

public interface ITransactionRepository
{
    public Task<TransactionModel> GetTransaction(string id);
    public void Save(TransactionModel transaction);
    public void Delete(string id);
}