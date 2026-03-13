using FinTracker.IDomain;

internal class TestOutbox : IDomainEventOutbox
{
    private readonly List<IDomainEvent> _events = [];

    public void Add<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent => _events.Add(domainEvent);

    public bool IsEventEmitted<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent => _events.Contains(domainEvent);
}