using FinTracker.Domain.Abstractions;

namespace FinTracker.Domain.Transaction.Events;

public record DefaultCategoryCreated(string CategoryId, string UserId) : IDomainEvent;