namespace FinTracker.Domain.Transaction.Model;

public class Transaction(TransactionId id, CategoryId categoryId, Money money, TransactionType type)
{
    public TransactionId Id { get; } = id;

    public CategoryId CategoryId { get; private set; } = categoryId;

    public Money Money { get; private set; } = money;

    public TransactionType Type { get; private set; } = type;

    public void ChangeMoneyAmount(Money newMoney) => Money = newMoney;

    public void ChangeCategory(CategoryId newCategoryId) => CategoryId = newCategoryId;

    public void ChangeType(TransactionType newType) => Type = newType;
}