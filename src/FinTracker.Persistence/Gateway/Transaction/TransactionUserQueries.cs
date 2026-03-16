using FinTracker.Transaction.Repository;
using Microsoft.EntityFrameworkCore;

internal class TransactionUserQueries(AppDbContext context, string userId) : IUserQueries
{
    private readonly IQueryable<TransactionModel> transactions = context.TransactionSchema.TransactionQuery;
    private readonly IQueryable<CategoryModel> categories = context.TransactionSchema.CategoryQuery;

    public async Task<IEnumerable<string>> GetUserCategories() => await categories
        .Where(c => c.UserId == userId)
        .Select(c => c.Id)
        .ToListAsync();

    public async Task<IEnumerable<TransactionModel>> GetUserTransactions() => await (
        from transaction in transactions
        join category in categories on transaction.CategoryId equals category.Id
        where category.Id == userId
        select transaction
        ).ToListAsync();



    // await context.Database
    //     .SqlQueryRaw<TransactionModel>(@"
    //         SELECT * FROM Transactions
    //         INNER JOIN Categories ON Transactions.CategoryId=Categories.Id
    //         WHERE Categories.UserId = {0}
    //     ", userId)
    //     .ToListAsync();

    public Task<TransactionModel> GetUserTransaction(string transactionId)
    {
        throw new NotImplementedException();
    }
}