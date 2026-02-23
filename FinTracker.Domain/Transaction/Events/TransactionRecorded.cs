using FinTracker.Domain.Abstractions;

namespace FinTracker.Domain.Transaction.Events;

public record TransactionRecorded(
    string TransactionId,
    string CategoryId,
    double MoneyAmount,
    string Currency
) : IDomainEvent;