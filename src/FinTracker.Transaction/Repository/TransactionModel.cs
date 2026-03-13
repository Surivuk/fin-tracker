namespace FinTracker.Transaction.Repository;

public record TransactionModel(string Id, string CategoryId, double MoneyAmount, string MoneyCurrency, string Type);
