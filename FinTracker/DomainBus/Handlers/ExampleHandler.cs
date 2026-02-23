using FinTracker.Domain.Abstractions;
using FinTracker.Domain.Transaction.Events;

namespace FinTracker.DomainBus.handlers;

public class ExampleHandler() : IDomainEventHandler<TransactionRecorded>
{
    public async Task Handle(TransactionRecorded e)
    {
        Console.WriteLine($"ExampleHandler is executed!!!");
    }
}
