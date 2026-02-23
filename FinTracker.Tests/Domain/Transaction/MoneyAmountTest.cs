using FinTracker.Domain.Transaction.Model;

namespace FinTracker.Tests.Domain.Transaction;

public class MoneyAmountTest
{
    [Fact]
    public void ValidMoneyAmount()
    {
        var amount = Money.New(100, Currency.EUR);

        Assert.Equal(100, amount.Amount);
        Assert.Equal("EUR", amount.Currency.Value);
    }

    [Fact]
    public void InvalidMoneyAmount()
    {
        Assert.Throws<InvalidMoneyAmount>(() => Money.New(0, Currency.EUR));
        Assert.Throws<InvalidMoneyAmount>(() => Money.New(-100, Currency.EUR));
    }

    [Fact]
    public void ValidCurrencyFormString()
    {
        var c = Currency.FromString("EUR");

        Assert.Equal("EUR", c.Value);
    }

    [Fact]
    public void InvalidCurrencyFromString()
    {
        Assert.Throws<InvalidCurrency>(() => Currency.FromString("USD"));
    }
}