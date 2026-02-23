using FinTracker.Domain.Abstractions;
using FinTracker.Persistence.Context;

namespace FinTracker.Command;

public readonly record struct CommandRequest(IDomainCommand<IDomainCommandRequestData> Command, IDomainCommandRequestData RequestData);

public class BatchCommandExecutor(AppDbContext Context)
{
    private readonly List<CommandRequest> _commandRequests = [];

    public void AddCommand<Cmd, CmdRequest>(Cmd command, CmdRequest request)
    where Cmd : IDomainCommand<CmdRequest> where CmdRequest : IDomainCommandRequestData =>
        _commandRequests.Add(new((IDomainCommand<IDomainCommandRequestData>)command, request));

    public async Task Execute()
    {
        foreach (var request in _commandRequests)
            await request.Command.Execute(request.RequestData);
            
        await Context.SaveChangesAsync();
    }
}




