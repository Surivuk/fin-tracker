namespace FinTracker.Domain.Transaction.Model;

public class InvalidMoneyAmount(double amount) : Exception($"Invalid money amount, it must be a positive number! Amount: \"{amount}\"");
public class InvalidCurrency(string value) : Exception($"Invalid currency! Currency: \"{value}\"");

public record Currency
{
    private static readonly string[] ValidCurrencies = ["EUR"];

    public string Value { get; private init; }

    private Currency(string value) => Value = value;

    public static Currency EUR => new(ValidCurrencies[0]);

    public static Currency FromString(string value)
    {
        if (!ValidCurrencies.Contains(value)) throw new InvalidCurrency(value);

        return new(value);
    }
}

public record Money
{
    public double Amount { get; private init; }

    public Currency Currency { get; private init; }

    private Money(double amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money New(double amount, Currency currency)
    {
        if (amount <= 0) throw new InvalidMoneyAmount(amount);

        return new(amount, currency);
    }

    public static Money Default => new(1, Currency.EUR);
}