using FinTracker.IDomain;

namespace FinTracker.Transaction.Events;

public record CategoryCreated(string CategoryId, string UserId) : IDomainEvent;
