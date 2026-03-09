using FinTracker.IDomain;
using FinTracker.Transaction;
using FinTracker.Transaction.Repository;
using Microsoft.Extensions.DependencyInjection;

internal class TestScope(IServiceScope scope)
{
    private readonly IServiceProvider _provider = scope.ServiceProvider;

    public static TestScope New => new(
        new ServiceCollection()
            .AddScoped<IDomainBus, TestBus>()
            .AddScoped<ICategoryRepository, DirectoryCategoryRepository>()
            .AddTransactionModel()
            .BuildServiceProvider()
            .CreateScope()
    );

    public T GetService<T>() where T : notnull => _provider.GetRequiredService<T>();
    public void Dispose() => scope.Dispose();

    public TestBus DomainBus => (TestBus)_provider.GetRequiredService<IDomainBus>();
}