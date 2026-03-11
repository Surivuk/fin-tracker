using FinTracker.DomainCore;
using FinTracker.IDomain;

internal readonly record struct CommandEmitChainData(string Identifier, string FirstHandlerIdentifier, string SecondHandlerIdentifier);

internal class CommandEmitChainBuilder(TestRepository repository, DomainEventOutbox outbox) : IDomainCommandBuilder<CommandEmitChainData>
{
    public DomainCommandResult TryWith(CommandEmitChainData data) => new(new CommandEmitChain(repository, outbox, data));
}

internal class CommandEmitChain : IDomainCommand
{
    private readonly TestRepository repository;
    private readonly DomainEventOutbox outbox;
    private readonly CommandEmitChainData data;

    internal CommandEmitChain(TestRepository repository, DomainEventOutbox outbox, CommandEmitChainData data)
    {
        this.repository = repository;
        this.outbox = outbox;
        this.data = data;
    }

    public async Task Execute()
    {
        repository.Save(data.Identifier);
        outbox.Add(new ChainEvent(data.FirstHandlerIdentifier, data.SecondHandlerIdentifier));
    }
}