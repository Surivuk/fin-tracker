using FinTracker.Domain.Abstractions;
using FinTracker.DomainBus;
using Microsoft.Extensions.DependencyInjection;

namespace FinTracker.Tests.App;

readonly record struct TestEvent() : IDomainEvent;

class TestHandler(ExecutionChecker checker) : IDomainEventHandler<TestEvent>
{
    public async Task Handle(TestEvent domainEvent) => checker.MarkAsExecuted(GetType());
}

public class DomainBusTest
{
    private readonly IServiceProvider _serviceProvider;

    public DomainBusTest()
    {
        _serviceProvider = new ServiceCollection()
            .AddSingleton((provider) => new DomainEventRegistry().RegisterHandler<TestEvent, TestHandler>())
            .AddScoped<HandlerFactory>()
            .AddScoped<TestHandler>()
            .AddScoped<ExecutionChecker>()
            .AddScoped<IDomainBus, DomainBus.DomainBus>()
            .BuildServiceProvider();
    }

    [Fact]
    public async Task EmitCycleShouldWork()
    {
        var domainBus = _serviceProvider.GetRequiredService<IDomainBus>();

        await domainBus.Emit(new TestEvent());

        Assert.True(_serviceProvider.GetRequiredService<ExecutionChecker>().IsExecuted<TestHandler>(1));
    }
}