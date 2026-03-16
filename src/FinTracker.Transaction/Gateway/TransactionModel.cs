namespace FinTracker.Transaction.Gateway;

public record TransactionModel(string Id, string CategoryId, double MoneyAmount, string MoneyCurrency, string Type);
