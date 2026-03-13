using FinTracker.IDomain;

internal class Category(EntityId id, EntityId userId)
{
    public EntityId Id { get; private init; } = id;

    public EntityId UserId { get; private init; } = userId;
}