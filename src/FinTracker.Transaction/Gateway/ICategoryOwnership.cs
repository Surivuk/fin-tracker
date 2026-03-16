namespace FinTracker.Transaction.Gateway;

public interface ICategoryOwnership
{
    public Task<bool> IsMyCategory(string userId, string categoryId);
}