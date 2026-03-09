using FinTracker.IDomain;

namespace FinTracker.Transaction.Events;

public record DefaultCategoryCreated(string CategoryId, string UserId) : IDomainEvent;
