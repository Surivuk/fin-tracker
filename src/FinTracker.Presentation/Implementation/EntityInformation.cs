
using FinTracker.IDomain;

internal class EntityInformationError(string message) : Exception($"Information is invalid. {message}");

internal readonly record struct EntityInformation
{
    public string Title { get; private init; }

    public string Description { get; private init; }

    private EntityInformation(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public static Result<EntityInformation> New(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title)) return new(new EntityInformationError("Title must not be null or empty!"));

        return new(new EntityInformation(title, description));
    }
}