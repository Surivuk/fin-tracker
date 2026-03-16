using FinTracker.IDomain;
using Microsoft.Extensions.DependencyInjection;

namespace FinTracker.Persistence;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, Func<IServiceProvider, string> getUserId)
    {
        return services
            .AddDbContext<AppDbContext>()

            // Presentation
            .AddScoped<Presentation.Gateway.IUserQueries>(p => new PresentationUserQueries(p.GetRequiredService<AppDbContext>(), getUserId(p)))
            .AddScoped<Presentation.Gateway.ICategoryRepository, PresentationCategoryRepository>()

            // Transaction
            .AddScoped<Transaction.Gateway.ICategoryRepository, TransactionCategoryRepository>()
            .AddScoped<Transaction.Gateway.ITransactionRepository, TransactionTransactionRepository>()
            .AddScoped<Transaction.Gateway.IUserQueries>(p => new TransactionUserQueries(p.GetRequiredService<AppDbContext>(), getUserId(p)))

            .AddScoped<Transaction.Gateway.ICategoryOwnership, CategoryOwnership>()
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}