using FinTracker.IDomain;
using Microsoft.Extensions.DependencyInjection;

namespace FinTracker.DomainCore;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDomainCoreModule(this IServiceCollection services) =>
        services
            .AddSingleton<DomainHandlerRegistryCollection>()
            .AddScoped<IDomainEventOutbox, DomainEventOutbox>()
            .AddScoped<DomainEventOutbox>(p => (DomainEventOutbox)p.GetRequiredService<IDomainEventOutbox>())
            .AddScoped<DomainCommandExecutor>()
            .AddScoped<DomainHandlerFactory>();
}