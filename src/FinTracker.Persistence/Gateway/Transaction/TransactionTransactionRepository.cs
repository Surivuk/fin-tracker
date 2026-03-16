using FinTracker.Transaction.Repository;

internal class TransactionTransactionRepository(AppDbContext context) : ITransactionRepository
{
    private readonly Repository<TransactionModel, string> repository = context.TransactionSchema.Transactions;

    public Task<TransactionModel> GetTransaction(string id) => repository.Find(id);

    public void Save(TransactionModel transaction) => repository.Save(transaction);

    public void Delete(string id) => repository.Delete(id);

}