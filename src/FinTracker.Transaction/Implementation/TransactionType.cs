using FinTracker.IDomain;

namespace FinTracker.Transaction.Implementation;

internal class InvalidTransactionType(string type) : Exception($"Invalid transaction type! Type: \"{type}\"");

internal readonly record struct TransactionType
{
    public string Value { get; private init; }

    private static readonly string[] ValidValues = ["INCOME", "EXPENSE"];

    private TransactionType(string value) => Value = value;

    public static TransactionType Income => new(ValidValues[0]);
    public static TransactionType Expense => new(ValidValues[1]);
    public static Result<TransactionType> TryParse(string value)
    {
        if (!ValidValues.Contains(value)) return new(new InvalidTransactionType(value));

        return new(new TransactionType(value));
    }
}
