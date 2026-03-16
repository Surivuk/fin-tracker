using FinTracker.IDomain;
using FinTracker.Transaction.Gateway;

internal static class TransactionConversions
{
    internal static TransactionEntity ToDataModel(this TransactionModel model)
    {
        var id = EntityId.TryParse(model.Id);
        var categoryId = EntityId.TryParse(model.CategoryId);
        var moneyAmount = MoneyAmount.TryParse(model.MoneyAmount);
        var moneyCurrency = Currency.TryParse(model.MoneyCurrency);
        var type = TransactionType.TryParse(model.Type);

        if (id.IsFailure) throw id.Error!;
        if (categoryId.IsFailure) throw categoryId.Error!;
        if (moneyAmount.IsFailure) throw moneyAmount.Error!;
        if (moneyCurrency.IsFailure) throw moneyCurrency.Error!;
        if (type.IsFailure) throw type.Error!;

        return new(id.Value, categoryId.Value, new(moneyAmount.Value, moneyCurrency.Value), type.Value);
    }

    internal static TransactionModel ToModel(this TransactionEntity e) =>
        new(e.Id.Value, e.CategoryId.Value, e.Money.Amount.Value, e.Money.Currency.Value, e.Type.Value);
}