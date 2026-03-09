using FinTracker.IDomain;

internal class TestBus : IDomainBus
{
    private readonly Dictionary<Type, List<IDomainEvent>> _events = [];

    public async Task Emit<T>(T domainEvent) where T : IDomainEvent
    {
        if (_events.TryGetValue(domainEvent.GetType(), out var eventList))
            eventList.Add(domainEvent);
        else
            _events.Add(typeof(T), [domainEvent]);
    }

    public bool IsEventEmitted<T>() => _events.ContainsKey(typeof(T));
    public List<IDomainEvent> GetEvents<T>()
    {
        var type = typeof(T);
        if (_events.TryGetValue(type, out var eventList)) return eventList;
        return [];
    }

}