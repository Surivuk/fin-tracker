using FinTracker.IDomain;

internal class InvalidName(string field, string value) :
    ArgumentException($"Invalid Name. \"{field}\" must not be null or empty or shorter then 2 characters, value: '{value}'.");

internal readonly record struct Name
{
    public readonly string FirstName { get; private init; }

    public readonly string LastName { get; private init; }

    private Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static Result<Name> New(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 2) return new(new InvalidName("FirstName", firstName));
        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 2) return new(new InvalidName("LastName", lastName));

        return new(new Name(firstName, lastName));
    }
}