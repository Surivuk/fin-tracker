using FinTracker.IDomain;
using Microsoft.Extensions.DependencyInjection;

namespace FinTracker.DomainCore;

public class DomainHandlerFactory(DomainHandlerRegistryCollection collection, IServiceProvider serviceProvider)
{
    public List<IDomainEventHandler<TEvent>> GetHandlers<TEvent>() where TEvent : IDomainEvent
    {
        List<IDomainEventHandler<TEvent>> result = [];
        var eventType = typeof(TEvent);

        if (!collection.Registrations.TryGetValue(eventType, out var handlers)) return result;


        foreach (var handler in handlers)
            result.Add((IDomainEventHandler<TEvent>)serviceProvider.GetRequiredService(handler));

        return result;
    }
}