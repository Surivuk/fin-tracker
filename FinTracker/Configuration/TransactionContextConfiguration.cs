using FinTracker.Domain.Transaction.Abstractions;
using FinTracker.Domain.Transaction.Commands;
using FinTracker.Persistence.Repository.Transaction;

namespace FinTracker.Configuration;

public static class TransactionContextConfiguration
{
    public static IServiceCollection AddTransactionContext(this IServiceCollection services) =>
        services
            .AddScoped<CreateCategoryBuilder>()
            .AddScoped<DeleteCategoryBuilder>()

            .AddScoped<ITransactionRepository, TransactionRepository>()
            .AddScoped<ICategoryRepository, CategoryRepository>();
}