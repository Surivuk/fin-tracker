using FinTracker.IDomain;

internal readonly record struct BasicCommandData(string Identifier);

internal class BasicCommandBuilder(TestRepository repository) : IDomainCommandBuilder<BasicCommandData>
{
    public DomainCommandResult TryWith(BasicCommandData data) => new(new BasicCommand(repository, data));
}

internal class BasicCommand : IDomainCommand
{
    private readonly TestRepository repository;
    private readonly BasicCommandData data;

    internal BasicCommand(TestRepository repository, BasicCommandData data)
    {
        this.repository = repository;
        this.data = data;
    }

    public async Task Execute()
    {
        repository.Save(data.Identifier);

    }
}