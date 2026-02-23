using FinTracker.Domain.Abstractions;
using FinTracker.Domain.Transaction.Events;
using FinTracker.DomainBus;
using FinTracker.DomainBus.handlers;

namespace FinTracker.Configuration;

public static class DomainBusConfiguration
{
    public static IServiceCollection AddDomainBus(this IServiceCollection services) =>
         services
            .AddSingleton((provider) => new DomainEventRegistry()
                // Connect the domain event with the handler
                .RegisterHandler<TransactionRecorded, ExampleHandler>()
            )
            .AddScoped<HandlerFactory>()

            // Register all domain handler in the DI
            .AddScoped<ExampleHandler>()

            .AddScoped<IDomainBus, DomainBus.DomainBus>();
}