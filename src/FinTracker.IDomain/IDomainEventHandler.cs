namespace FinTracker.IDomain;

public interface IDomainEventHandler<IDomainEvent>
{
    public Task Handle(IDomainEvent domainEvent);
}