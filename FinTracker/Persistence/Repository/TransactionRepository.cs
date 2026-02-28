using FinTracker.Domain.Transaction.Abstractions;
using FinTracker.Domain.Transaction.Model;
using FinTracker.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinTracker.Persistence.Repository;

public class TransactionRepository(AppDbContext context) : ITransactionRepository
{
    public readonly DbSet<Transaction> _dbSet = context.TransactionTransactions;

    public async Task<Transaction> GetTransaction(TransactionId id)
    {
        var transaction = await _dbSet.FirstAsync(t => t.Id == id) ?? throw new Exception($"Transaction not found! Id: \"{id}\"");
        return transaction;
    }
    public void Save(Transaction transaction)
    {
        var entry = context.Entry(transaction);

        if (entry.State == EntityState.Detached) _dbSet.Add(transaction);

    }
    public void Delete(TransactionId id)
    {
        var transaction = _dbSet.Local.FirstOrDefault(t => t.Id == id);

        transaction ??= CreateStub(id);

        _dbSet.Remove(transaction);
    }

    private static Transaction CreateStub(TransactionId id) => new(id, CategoryId.Empty, Money.New(1, Currency.EUR), TransactionType.Income);

}



