namespace FinTracker.Domain.Presentation.Model;

public class EntityInformationError(string message) : Exception($"Information is invalid. {message}");

public readonly record struct EntityInformation
{
    public string Title { get; private init; }

    public string Description { get; private init; }

    private EntityInformation(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public static EntityInformation New(string title, string description)
    {
        if (string.IsNullOrEmpty(title)) throw new EntityInformationError("Title must not be null or empty!");

        return new(title, description);
    }
}