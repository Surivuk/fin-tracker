namespace FinTracker.IDomain;

public interface IDomainCommand
{
    public Task Execute();
}

public interface IDomainCommandBuilder<Data>
{
    public DomainCommandResult TryWith(Data data);
}


