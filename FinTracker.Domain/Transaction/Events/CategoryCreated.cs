using FinTracker.Domain.Abstractions;

namespace FinTracker.Domain.Transaction.Events;

public record CategoryCreated(string CategoryId) : IDomainEvent;