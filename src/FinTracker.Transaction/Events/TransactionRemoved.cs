using FinTracker.IDomain;

namespace FinTracker.Transaction.Events;

public record TransactionRemoved(string TransactionId) : IDomainEvent;
