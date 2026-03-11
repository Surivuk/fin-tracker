using FinTracker.IDomain;
using FinTracker.TestKit;
using Microsoft.Extensions.DependencyInjection;

namespace FinTracker.DomainCore.Test;

public sealed class CommandExecutor : IDisposable
{
    private readonly TestScope _scope = TestScope.New(c => c
        .AddDomainCoreModule()
        .AddScoped<TestUnitOfWork>()
        .AddScoped<IUnitOfWork>(p => p.GetRequiredService<TestUnitOfWork>())
        .AddScoped<TestRepository>()

        .AddScoped<BasicCommandBuilder>()
        .AddScoped<CommandEmitBasicBuilder>()
        .AddScoped<CommandEmitChainBuilder>()

        .AddScoped<BasicHandler>()
        .AddScoped<HandlerThatEmit>()

        .AddSingleton<IDomainHandlerRegistry>(p => new DomainEventRegistry()
            .RegisterHandler<BasicEvent, BasicHandler>()
            .RegisterHandler<ChainEvent, HandlerThatEmit>()
        )
    );
    public void Dispose() => _scope.Dispose();

    [Fact]
    public async Task Should_be_one_unit()
    {
        var executor = _scope.GetService<DomainCommandExecutor>();
        var cmd = _scope.GetService<BasicCommandBuilder>().TryWith(new("FIRST_COMMAND")).Command!;
        var unitOfWork = _scope.GetService<TestUnitOfWork>();

        await executor.ExecuteAsync(cmd);

        Assert.Equal(1, unitOfWork.NumberOfUnits);
        Assert.Equal(["FIRST_COMMAND"], unitOfWork.GetExecutionUnit(0));
    }

    [Fact]
    public async Task Should_be_one_unit_with_two_executions()
    {
        var executor = _scope.GetService<DomainCommandExecutor>();
        var cmdBuilder = _scope.GetService<BasicCommandBuilder>();
        var unitOfWork = _scope.GetService<TestUnitOfWork>();

        await executor.ExecuteAsync([
            cmdBuilder.TryWith(new("FIRST_COMMAND")).Command!,
            cmdBuilder.TryWith(new("SECOND_COMMAND")).Command!,
        ]);

        Assert.Equal(1, unitOfWork.NumberOfUnits);
        Assert.Equal(["FIRST_COMMAND", "SECOND_COMMAND"], unitOfWork.GetExecutionUnit(0));
    }

    [Fact]
    public async Task Should_be_two_units()
    {
        var executor = _scope.GetService<DomainCommandExecutor>();
        var basicCmd = _scope.GetService<BasicCommandBuilder>().TryWith(new("FIRST_COMMAND")).Command!;
        var eventCmd = _scope.GetService<CommandEmitBasicBuilder>().TryWith(new("SECOND_COMMAND", "FIRST_HANDLER")).Command!;
        var unitOfWork = _scope.GetService<TestUnitOfWork>();

        await executor.ExecuteAsync([basicCmd, eventCmd]);

        Assert.Equal(2, unitOfWork.NumberOfUnits);
        Assert.Equal(["FIRST_COMMAND", "SECOND_COMMAND"], unitOfWork.GetExecutionUnit(0));
        Assert.Equal(["FIRST_HANDLER"], unitOfWork.GetExecutionUnit(1));
    }

    [Fact]
    public async Task Should_be_three_units()
    {
        var executor = _scope.GetService<DomainCommandExecutor>();
        var cmd_1 = _scope.GetService<CommandEmitBasicBuilder>().TryWith(new("FIRST_COMMAND", "FIRST_HANDLER")).Command!;
        var cmd_2 = _scope.GetService<CommandEmitChainBuilder>().TryWith(new("SECOND_COMMAND", "SECOND_HANDLER", "THIRD_HANDLER")).Command!;
        var unitOfWork = _scope.GetService<TestUnitOfWork>();

        await executor.ExecuteAsync([cmd_1, cmd_2]);

        Assert.Equal(3, unitOfWork.NumberOfUnits);
        Assert.Equal(["FIRST_COMMAND", "SECOND_COMMAND"], unitOfWork.GetExecutionUnit(0));
        Assert.Equal(["FIRST_HANDLER", "SECOND_HANDLER"], unitOfWork.GetExecutionUnit(1));
        Assert.Equal(["THIRD_HANDLER"], unitOfWork.GetExecutionUnit(2));
    }

}
