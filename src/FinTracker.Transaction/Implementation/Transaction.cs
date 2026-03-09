using FinTracker.IDomain;

namespace FinTracker.Transaction.Implementation;

internal class TransactionEntity(EntityId id, EntityId categoryId, Money money, TransactionType type)
{
    public EntityId Id { get; private init; } = id;

    public EntityId CategoryId { get; private set; } = categoryId;

    public Money Money { get; private set; } = money;

    public TransactionType Type { get; private set; } = type;

    public void ChangeMoneyAmount(Money newMoney) => Money = newMoney;

    public void ChangeCategory(EntityId newCategoryId) => CategoryId = newCategoryId;

    public void ChangeType(TransactionType newType) => Type = newType;
}