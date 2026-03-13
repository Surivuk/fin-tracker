namespace FinTracker.Presentation.Gateway;

public record UserCategoryModel(string Id, string Title, string? Description, string Color);

public interface IUserQueries
{
    public Task<UserCategoryModel> GetUserCategory(string categoryId);

    public Task<IReadOnlyList<UserCategoryModel>> GetUserCategories();

    // public Task<IEnumerable<TransactionModel>> GetUserTransactions();

    // public Task<TransactionModel> GetUserTransaction(string transactionId);
}