namespace FinTracker.Domain.Transaction.Model;

public class Category(CategoryId id, string userId)
{
    public CategoryId Id { get; private init; } = id;

    public string UserId { get; private init; } = userId;
}