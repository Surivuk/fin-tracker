using FinTracker.Domain.Transaction.Abstractions;
using FinTracker.Domain.Transaction.Model;
using FinTracker.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinTracker.Persistence.Repository;

public class TransactionRepository : ITransactionRepository
{

    public readonly AppDbContext context;

    public TransactionRepository(AppDbContext context)
    {
        this.context = context;
        Console.WriteLine("TransactionRepository is created!");
    }

    public async Task<Transaction> GetTransaction(TransactionId id)
    {
        var transaction = await context.Transactions.FirstAsync(t => t.Id == id);

        if (transaction is null) throw new Exception($"Not found transaction! Id: \"{id}\"");

        return transaction;
    }
    public void Save(Transaction transaction)
    {
        var entry = context.Entry(transaction);

        if (entry.State == EntityState.Detached) context.Transactions.Add(transaction);

    }
    public void Delete(TransactionId id)
    {
        var transaction = context.Transactions.Local.FirstOrDefault(t => t.Id == id);

        transaction ??= CreateStub(id);

        context.Transactions.Remove(transaction);
    }

    private Transaction CreateStub(TransactionId id)
    {
        var entry = context.Transactions.Entry((Transaction)Activator.CreateInstance(typeof(Transaction), nonPublic: true)!);

        entry.Property(t => t.Id).CurrentValue = id;
        entry.State = EntityState.Deleted;

        return entry.Entity;
    }
}



