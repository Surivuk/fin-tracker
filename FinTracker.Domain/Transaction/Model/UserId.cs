namespace FinTracker.Domain.Transaction.Model;

public class InvalidUserId(string value) : ArgumentException($"Invalid value for user id. Value: '{value}'.");

public readonly record struct UserId
{
    public string Value { get; private init; }

    private UserId(string value) => Value = value;

    public static UserId From(string value)
    {
        if (!Guid.TryParse(value, out var guid)) throw new InvalidUserId(value);

        return new(guid.ToString());
    }

    public override string ToString() => Value.ToString();
}
