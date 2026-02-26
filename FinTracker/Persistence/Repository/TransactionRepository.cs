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
        var transaction = await context.Transactions.FirstAsync(t => t.Id == id) ?? throw new Exception($"Transaction not found! Id: \"{id}\"");
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

    private static Transaction CreateStub(TransactionId id) => new(id, CategoryId.Empty, Money.New(1, Currency.EUR), TransactionType.Income);

}



