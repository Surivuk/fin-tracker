using FinTracker.DomainCore;
using FinTracker.IDomain;

internal class HandlerThatEmit(TestRepository repository, DomainEventOutbox outbox) : IDomainEventHandler<ChainEvent>
{
    public async Task Handle(ChainEvent domainEvent)
    {
        repository.Save(domainEvent.Identifier);
        outbox.Add(new BasicEvent(domainEvent.HandlerIdentifier));
    }
}