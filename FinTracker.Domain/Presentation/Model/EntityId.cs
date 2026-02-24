namespace FinTracker.Domain.Presentation.Model;

public class InvalidEntityId(string value) : ArgumentException($"Invalid value for Entity Id. Value: '{value}'.");

public readonly record struct EntityId
{
    public string Value { get; private init; }

    private EntityId(string value) => Value = value;

    public static EntityId From(string value)
    {
        if (!Guid.TryParse(value, out var guid)) throw new InvalidEntityId(value);

        return new(guid.ToString());
    }
}