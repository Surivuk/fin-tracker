namespace FinTracker.Domain.Transaction.Model;

public class InvalidTransactionType(string type) : Exception($"Invalid transaction type! Type: \"{type}\"");

public readonly record struct TransactionType
{
    public string Value { get; private init; }

    private static readonly string[] ValidValues = ["INCOME", "EXPENSE"];

    private TransactionType(string value) => Value = value;

    public static TransactionType Income => new(ValidValues[0]);
    public static TransactionType Expense => new(ValidValues[1]);
    public static TransactionType FromString(string value)
    {
        if (!ValidValues.Contains(value)) throw new InvalidTransactionType(value);
        return new TransactionType(value);
    }
}
