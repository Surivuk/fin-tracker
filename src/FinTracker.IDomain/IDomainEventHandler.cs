namespace FinTracker.IDomain;

public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
{
    public Task Handle(TEvent domainEvent);
}