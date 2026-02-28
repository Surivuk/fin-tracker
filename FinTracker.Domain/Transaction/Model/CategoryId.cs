namespace FinTracker.Domain.Transaction.Model;

public readonly record struct CategoryId(Guid Value)
{
    public static CategoryId Empty => new(Guid.Empty);
    public static CategoryId New => new(Guid.NewGuid());
    public static CategoryId Parse(string value) => new(Guid.Parse(value));

    public override string ToString() => Value.ToString();
}
