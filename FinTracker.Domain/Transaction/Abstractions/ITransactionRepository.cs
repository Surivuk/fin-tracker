using FinTracker.Domain.Transaction.Model;

namespace FinTracker.Domain.Transaction.Abstractions;

using TheTransaction = Model.Transaction;

public interface ITransactionRepository
{
    public Task<TheTransaction> GetTransaction(TransactionId id);
    public void Save(TheTransaction transaction);
    public void Delete(TransactionId id);
}