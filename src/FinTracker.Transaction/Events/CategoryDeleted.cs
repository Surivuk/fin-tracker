using FinTracker.IDomain;

namespace FinTracker.Transaction.Events;

public record CategoryDeleted(string CategoryId) : IDomainEvent;
