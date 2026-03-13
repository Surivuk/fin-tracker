using FinTracker.IDomain;
using FinTracker.TestKit;
using FinTracker.Presentation;
using FinTracker.Presentation.Gateway;
using Microsoft.Extensions.DependencyInjection;

internal static class TestSetup
{
    public static TestScope CreateScope(string userId) => TestScope.New(c => c
        .AddScoped<InMemory>()
        .AddScoped<IDomainEventOutbox, TestOutbox>()
        .AddScoped<ICategoryRepository, CategoryRepository>()
        .AddScoped<IUserQueries, UserQueries>(p => new UserQueries(p.GetRequiredService<InMemory>(), userId))
        .AddPresentationModel()
    );
}