using FinTracker.Command;

namespace FinTracker.Configuration;

public static class CommandsConfiguration
{
    public static IServiceCollection AddCommands(this IServiceCollection services) =>
        services.AddScoped<CommandExecutor>();
}