namespace FinTracker.IDomain;

public interface IDomainEventOutbox
{
    public void Add<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
}