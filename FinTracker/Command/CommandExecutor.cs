using FinTracker.Domain.Abstractions;
using FinTracker.Persistence.Context;

namespace FinTracker.Command;

public class CommandExecutor(AppDbContext Context)
{
    public async Task ExecuteAsync(params IDomainCommand[] commands)
    {
        foreach (var command in commands)
            await command.Execute();

        await Context.SaveChangesAsync();
    }
}