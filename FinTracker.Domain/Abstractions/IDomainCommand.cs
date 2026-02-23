namespace FinTracker.Domain.Abstractions;

public interface IDomainCommandRequestData;

public interface IDomainCommand<CmdData> where CmdData: IDomainCommandRequestData
{
    public Task Execute(CmdData requestData);
}
