namespace FinTracker.Domain.Abstractions;

public interface IDomainCommand
{
    public Task Execute();
}

public interface IDomainCommandBuilder<Data>
{
    public IDomainCommand With(Data data);
}


