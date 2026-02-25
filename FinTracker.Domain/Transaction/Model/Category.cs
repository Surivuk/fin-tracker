namespace FinTracker.Domain.Transaction.Model;

public class Category(CategoryId id, UserId userId)
{
    public CategoryId Id { get; private init; } = id;

    public UserId UserId { get; private init; } = userId;
}