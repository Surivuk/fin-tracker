using FinTracker.IDomain;

namespace FinTracker.Transaction.Events;

public record TransactionRecorded(string TransactionId, string CategoryId, double MoneyAmount, string Currency) : IDomainEvent;
