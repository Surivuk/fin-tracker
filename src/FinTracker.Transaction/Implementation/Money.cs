using FinTracker.IDomain;

namespace FinTracker.Transaction.Implementation;

internal class InvalidMoneyAmount(double amount) : Exception($"Invalid money amount, it must be a positive number! Amount: \"{amount}\"");

internal class InvalidCurrency(string value) : Exception($"Invalid currency! Currency: \"{value}\"");

internal readonly record struct Currency
{
    private static readonly string[] ValidCurrencies = ["EUR"];

    public string Value { get; private init; }

    private Currency(string value) => Value = value;

    public static Currency EUR => new(ValidCurrencies[0]);

    public static Result<Currency> TryParse(string value)
    {
        if (!ValidCurrencies.Contains(value))
            return new(new InvalidCurrency(value));

        return new(new Currency(value));
    }
}

internal readonly record struct MoneyAmount
{
    public double Value { get; private init; }

    private MoneyAmount(double value) => Value = value;

    public static Result<MoneyAmount> TryParse(double amount)
    {
        if (amount <= 0)
            return new(new InvalidMoneyAmount(amount));

        return new(new MoneyAmount(amount));
    }
}

internal readonly record struct Money(MoneyAmount Amount, Currency Currency);