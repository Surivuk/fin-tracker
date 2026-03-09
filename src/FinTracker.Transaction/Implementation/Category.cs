using FinTracker.IDomain;

namespace FinTracker.Transaction.Implementation;

internal class Category(EntityId id, EntityId userId)
{
    public EntityId Id { get; private init; } = id;

    public EntityId UserId { get; private init; } = userId;
}