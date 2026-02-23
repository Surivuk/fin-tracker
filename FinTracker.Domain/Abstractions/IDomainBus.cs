namespace FinTracker.Domain.Abstractions;

public interface IDomainEvent;

public interface IDomainBus
{
    public Task Emit<T>(T domainEvent) where T: IDomainEvent;
}