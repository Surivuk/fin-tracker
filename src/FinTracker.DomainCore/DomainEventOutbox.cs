using FinTracker.IDomain;

namespace FinTracker.DomainCore;

// public class DomainEventOutbox : IDomainEventOutbox
// {
//     private readonly List<IDomainEvent> events = [];

//     public void Add<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent => events.Add(domainEvent);

//     public IReadOnlyList<IDomainEvent> NextBatch()
//     {
//         var result = events.ToList().AsReadOnly();

//         events.Clear();

//         return result;
//     }

//     public bool IsEmpty => events.Count == 0;
// }

public class DomainEventOutbox(DomainHandlerFactory handlerFactory) : IDomainEventOutbox
{
    private readonly List<Func<Task>> events = [];

    public void Add<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
    {
        var handlers = handlerFactory.GetHandlers<TEvent>();
        foreach (var handler in handlers)
            events.Add(() => handler.Handle(domainEvent));
    }

    public IReadOnlyList<Func<Task>> NextBatch()
    {
        var result = events.ToList().AsReadOnly();

        events.Clear();

        return result;
    }

    public bool IsEmpty => events.Count == 0;
}
