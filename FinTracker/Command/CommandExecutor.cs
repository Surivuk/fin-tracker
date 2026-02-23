using FinTracker.Domain.Abstractions;
using FinTracker.Persistence.Context;

namespace FinTracker.Command;

public class CommandExecutor<Cmd, CmdData>(AppDbContext Context, Cmd Command) 
where Cmd : IDomainCommand<CmdData> where CmdData : IDomainCommandRequestData
{
    public async Task Execute(CmdData requestData)
    {
        await Command.Execute(requestData);

        await Context.SaveChangesAsync();
    }
}
