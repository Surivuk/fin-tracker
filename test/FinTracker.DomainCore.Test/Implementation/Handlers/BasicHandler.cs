using FinTracker.IDomain;

internal class BasicHandler(TestRepository repository) : IDomainEventHandler<BasicEvent>
{
    public async Task Handle(BasicEvent domainEvent)
    {
        repository.Save(domainEvent.Identifier);
    }
}