using FinTracker.Domain.Transaction.Model;

using TheTransaction = FinTracker.Domain.Transaction.Model.Transaction;

namespace FinTracker.Tests.Domain.Transaction;

public class TransactionTest
{
    [Fact]
    public void ValidTransaction()
    {
        var id = TransactionId.New;
        var t = new TheTransaction(id, CategoryId.New, Money.New(100, Currency.EUR), TransactionType.Income);

        Assert.Equal(id, t.Id);
    }

    [Fact]
    public void TransactionShouldBeUpdated()
    {
        var newCategory = CategoryId.New;
        var newAmount = Money.New(500, Currency.EUR);
        var newType = TransactionType.Expense;

        var transaction = new TheTransaction(TransactionId.New, CategoryId.New, Money.New(100, Currency.EUR), TransactionType.Income);

        transaction.ChangeCategory(newCategory);
        transaction.ChangeMoneyAmount(newAmount);
        transaction.ChangeType(newType);

        Assert.Equal(newCategory, transaction.CategoryId);
        Assert.Equal(newAmount, transaction.Money);
        Assert.Equal(newType, transaction.Type);
    }

    [Fact]
    public void ValidTransactionTypes()
    {
        Assert.Null(Record.Exception(() => TransactionType.Income));
        Assert.Null(Record.Exception(() => TransactionType.Expense));
        Assert.Null(Record.Exception(() => TransactionType.FromString("INCOME")));
        Assert.Null(Record.Exception(() => TransactionType.FromString("EXPENSE")));
    }

    [Fact]
    public void InvalidTransactionType()
    {
        Assert.Throws<InvalidTransactionType>(() => TransactionType.FromString("ANY VALUE EXCEPT 'INCOME' OR 'EXPENSE'"));
    }
}