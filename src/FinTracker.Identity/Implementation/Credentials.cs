using System.Text.RegularExpressions;
using FinTracker.IDomain;

internal class InvalidPassword(string message) : Exception(message);

internal readonly partial record struct Credentials
{
    private readonly string Value { get; init; }

    private Credentials(string value) => Value = value;

    public static Result<Credentials> New(string value)
    {
        if (!PasswordRegex().IsMatch(value)) return new(CreatePasswordException(value));

        return new(new Credentials(value));
    }

    private static InvalidPassword CreatePasswordException(string password)
    {
        List<string> errors = [];

        if (password.Length < 8) errors.Add("at least 8 characters");
        if (!password.Any(char.IsLower)) errors.Add("one lowercase letter");
        if (!password.Any(char.IsUpper)) errors.Add("one uppercase letter");
        if (!password.Any(char.IsDigit)) errors.Add("one number");
        if (!password.Any(c => "@$!%*?&".Contains(c))) errors.Add("one special character (@$!%*?&)");

        if (errors.Count == 0) return new InvalidPassword($"Unknown password error, value: \"{password}\"");

        return new InvalidPassword($"Password must contain: {string.Join(", ", errors)}.");
    }

    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")]
    private static partial Regex PasswordRegex();

    public override string ToString() => Value;
}