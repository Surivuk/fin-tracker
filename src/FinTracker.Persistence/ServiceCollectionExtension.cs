using FinTracker.IDomain;
using Microsoft.Extensions.DependencyInjection;

namespace FinTracker.Persistence;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, Func<IServiceProvider, string> getUserId)
    {
        return services
            .AddDbContext<AppDbContext>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<Presentation.Gateway.IUserQueries>(p => new PresentationUserQueries(p.GetRequiredService<AppDbContext>(), getUserId(p)))
            .AddScoped<Presentation.Gateway.ICategoryRepository, PresentationCategoryRepository>()

            .AddScoped<Transaction.Repository.ICategoryRepository, TransactionCategoryRepository>();
    }
}