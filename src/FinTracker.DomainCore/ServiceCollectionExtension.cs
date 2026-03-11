using Microsoft.Extensions.DependencyInjection;

namespace FinTracker.DomainCore;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDomainCoreModule(this IServiceCollection services) =>
        services
            .AddSingleton<DomainHandlerRegistryCollection>()
            .AddScoped<DomainEventOutbox>()
            .AddScoped<DomainCommandExecutor>()
            .AddScoped<DomainHandlerFactory>();
}