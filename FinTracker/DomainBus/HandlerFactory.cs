using FinTracker.Domain.Abstractions;

namespace FinTracker.DomainBus;

public class HandlerFactory(DomainEventRegistry registry, IServiceProvider serviceProvider)
{
    public List<IDomainEventHandler<TEvent>> GetHandlers<TEvent>() where TEvent: IDomainEvent
    {
        List<IDomainEventHandler<TEvent>> result = [];
        var eventType = typeof(TEvent);

        if (!registry.HandlerRegistrations.TryGetValue(eventType, out var handlers)) return result;

        foreach (var handler in handlers)
            result.Add((IDomainEventHandler<TEvent>)serviceProvider.GetRequiredService(handler));

        return result;
    }
}