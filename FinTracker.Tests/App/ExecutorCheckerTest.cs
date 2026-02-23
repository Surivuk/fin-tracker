namespace FinTracker.Tests.App;


public class ExecutorCheckerTest
{
    [Fact]
    public async Task ShouldBeExecuted()
    {
        var checker = new ExecutionChecker();

        checker.MarkAsExecuted(GetType());

        Assert.True(checker.IsExecuted<ExecutorCheckerTest>());
    }

    [Fact]
    public async Task ShouldNotBeExecuted()
    {
        var checker = new ExecutionChecker();

        Assert.False(checker.IsExecuted<ExecutorCheckerTest>());
    }

    [Fact]
    public async Task ShouldBeExecuteOnce()
    {
        var checker = new ExecutionChecker();

        checker.MarkAsExecuted(GetType());

        Assert.True(checker.IsExecuted<ExecutorCheckerTest>(1));
    }

    [Fact]
    public async Task ShouldBeExecuteTwice()
    {
        var checker = new ExecutionChecker();

        checker.MarkAsExecuted(GetType());
        checker.MarkAsExecuted(GetType());

        Assert.True(checker.IsExecuted<ExecutorCheckerTest>(2));
    }
}