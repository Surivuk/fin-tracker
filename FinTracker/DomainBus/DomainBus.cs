using FinTracker.Domain.Abstractions;

namespace FinTracker.DomainBus;

public class DomainBusException(string message, IEnumerable<Exception> innerExceptions) : AggregateException(message, innerExceptions);

public class DomainBus(HandlerFactory handlerFactory) : IDomainBus
{
    public async Task Emit<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
    {
        var eventType = domainEvent.GetType();
        var handlers = handlerFactory.GetHandlers<TEvent>();

        List<Exception>? errors = null;
        foreach (var handler in handlers)
        {
            try
            {
                await handler.Handle(domainEvent);
            }
            catch (Exception ex)
            {
                errors ??= [ex];
            }
        }

        if (errors is not null) throw new DomainBusException($"One or more handlers failed for event '{eventType.Name}'.", errors);
    }
}