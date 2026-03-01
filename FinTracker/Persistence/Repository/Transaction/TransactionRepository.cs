using FinTracker.Domain.Transaction.Abstractions;
using FinTracker.Domain.Transaction.Model;
using FinTracker.Persistence.Context;

namespace FinTracker.Persistence.Repository.Transaction;

using TheTransaction = Domain.Transaction.Model.Transaction;

public class TransactionRepository(AppDbContext context) : ITransactionRepository
{
    private readonly Repository<TheTransaction, TransactionId> _repo = context.TransactionTransactions;

    public Task<TheTransaction> GetTransaction(TransactionId id) => _repo.Find(id);
    public void Save(TheTransaction category) => _repo.Save(category);
    public void Delete(TransactionId id) => _repo.Delete(id);
}



