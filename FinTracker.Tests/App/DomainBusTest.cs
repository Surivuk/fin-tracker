using FinTracker.Domain.Abstractions;
using FinTracker.DomainBus;
using Microsoft.Extensions.DependencyInjection;

namespace FinTracker.Tests.App;

readonly record struct TestEvent() : IDomainEvent;

class TestHandler(ExecutorChecker checker) : IDomainEventHandler<TestEvent>
{
    public async Task Handle(TestEvent domainEvent) => checker.MarkAsExecuted();
}

class ExecutorChecker
{
    public bool IsExecute { get; private set; } = false;

    public void MarkAsExecuted() => IsExecute = true;
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
            .AddScoped<ExecutorChecker>()
            .AddScoped<IDomainBus, DomainBus.DomainBus>()
            .BuildServiceProvider();
    }

    [Fact]
    public async Task EmitCycleShouldWork()
    {
        var domainBus = _serviceProvider.GetRequiredService<IDomainBus>();

        await domainBus.Emit(new TestEvent());

        Assert.True(_serviceProvider.GetRequiredService<ExecutorChecker>().IsExecute);
    }
}