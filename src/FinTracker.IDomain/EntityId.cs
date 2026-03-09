using System.Reflection.Metadata.Ecma335;

namespace FinTracker.IDomain;

public class InvalidEntityId(string value) : ArgumentException($"Invalid value for Entity Id. Value: '{value}'.");

public readonly record struct EntityId
{
    public string Value { get; private init; }

    private EntityId(string value) => Value = value;

    public static Result<EntityId> TryParse(string value)
    {
        if (!Guid.TryParse(value, out var guid))
            return new(new InvalidEntityId(value));

        return new(new EntityId(guid.ToString()));
    }

    public static EntityId New => new(Guid.NewGuid().ToString());
    public static string NewEntityString() => New.Value;

    public override string ToString() => Value;
}