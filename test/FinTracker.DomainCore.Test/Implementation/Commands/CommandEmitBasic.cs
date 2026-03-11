using FinTracker.DomainCore;
using FinTracker.IDomain;

internal readonly record struct CommandEmitBasicData(string Identifier, string HandlerIdentifier);

internal class CommandEmitBasicBuilder(TestRepository repository, DomainEventOutbox outbox) : IDomainCommandBuilder<CommandEmitBasicData>
{
    public DomainCommandResult TryWith(CommandEmitBasicData data) => new(new CommandEmitBasic(repository, outbox, data));
}

internal class CommandEmitBasic : IDomainCommand
{
    private readonly TestRepository repository;
    private readonly DomainEventOutbox outbox;
    private readonly CommandEmitBasicData data;

    internal CommandEmitBasic(TestRepository repository, DomainEventOutbox outbox, CommandEmitBasicData data)
    {
        this.repository = repository;
        this.outbox = outbox;
        this.data = data;
    }

    public async Task Execute()
    {
        repository.Save(data.Identifier);
        outbox.Add(new BasicEvent(data.HandlerIdentifier));
    }
}