using FinTracker.Persistence.Context;

namespace FinTracker.Configuration;

public static class AppConfiguration
{
    public static IServiceCollection AddAppServices(this IServiceCollection services) =>
        services
            .AddDbContext<AppDbContext>()
            .AddDomainBus()
            .AddCommands()

            .AddTransactionContext()
            .AddPresentationContext();
}