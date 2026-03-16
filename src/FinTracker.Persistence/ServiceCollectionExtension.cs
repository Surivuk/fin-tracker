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
            .AddScoped<Transaction.Repository.ICategoryRepository, TransactionCategoryRepository>()
            .AddScoped<Transaction.Repository.ITransactionRepository, TransactionTransactionRepository>()
            .AddScoped<Transaction.Repository.IUserQueries>(p => new TransactionUserQueries(p.GetRequiredService<AppDbContext>(), getUserId(p)))

            .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}