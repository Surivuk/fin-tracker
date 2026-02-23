namespace FinTracker.Domain.Abstractions;

public interface IDomainEventHandler<IDomainEvent>
{
    public Task Handle(IDomainEvent domainEvent);
}