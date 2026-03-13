using FinTracker.IDomain;
using FinTracker.TestKit;
using FinTracker.Transaction;
using FinTracker.Transaction.Repository;
using Microsoft.Extensions.DependencyInjection;

internal static class TestSetup
{
    public static TestScope CreateScope(string userId) => TestScope.New(c => c
        .AddScoped<InMemory>()
        .AddScoped<IDomainEventOutbox, TestOutbox>()
        .AddScoped<ICategoryRepository, CategoryRepository>()
        .AddScoped<IUserQueries, UserQueries>(p => new UserQueries(p.GetRequiredService<InMemory>(), userId))
        .AddTransactionModel()
    );
}