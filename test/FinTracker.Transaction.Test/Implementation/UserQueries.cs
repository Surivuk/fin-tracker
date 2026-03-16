using FinTracker.Transaction.Gateway;

internal class UserQueries(InMemory memory, string UserId) : IUserQueries
{
    private readonly Dictionary<string, CategoryModel> categories = memory.Categories;

    public async Task<IEnumerable<string>> GetUserCategories() => categories.Values.Where(c => c.UserId == UserId).Select(c => c.Id);

    public Task<IEnumerable<TransactionModel>> GetUserTransactions()
    {
        throw new NotImplementedException();
    }

    public Task<TransactionModel> GetUserTransaction(string transactionId)
    {
        throw new NotImplementedException();
    }
}