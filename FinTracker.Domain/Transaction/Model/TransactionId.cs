namespace FinTracker.Domain.Transaction.Model;

public readonly record struct TransactionId(Guid Value)
{
    public static TransactionId Empty => new(Guid.Empty);
    public static TransactionId New => new(Guid.NewGuid());
    public static TransactionId Parse(string value) => new(Guid.Parse(value));
}
