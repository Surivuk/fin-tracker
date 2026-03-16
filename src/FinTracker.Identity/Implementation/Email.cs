using System.Text.RegularExpressions;
using FinTracker.IDomain;

internal class InvalidEmail(string value) : Exception($"Invalid email. Value: \"{value}\"");

internal readonly partial record struct Email
{
    private readonly string Value { get; init; }

    private Email(string value) => Value = value;

    public static Result<Email> New(string value)
    {
        if (!EmailRegex().IsMatch(value)) return new(new InvalidEmail(value));

        return new(new Email(value));
    }

    [GeneratedRegex(@"^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$")]
    private static partial Regex EmailRegex();

    public override string ToString() => Value;
}