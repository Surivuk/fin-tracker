namespace FinTracker.IDomain;

public class DomainEventRegistry : IDomainHandlerRegistry
{
    private readonly Dictionary<Type, HashSet<Type>> _registrations = [];
    public IReadOnlyDictionary<Type, HashSet<Type>> HandlerRegistrations => _registrations;

    public DomainEventRegistry RegisterHandler<TEvent, THandler>() where TEvent : IDomainEvent where THandler : IDomainEventHandler<TEvent>
    {
        var handlerType = typeof(THandler);
        var eventType = typeof(TEvent);

        if (_registrations.TryGetValue(eventType, out var registration))
        {
            registration.Add(handlerType);
        }
        else
        {
            _registrations.Add(eventType, [handlerType]);
        }

        return this;
    }
}