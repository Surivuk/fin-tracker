using FinTracker.Domain.Transaction.Abstractions;
using FinTracker.Persistence.Repository;

namespace FinTracker.Configuration;

public static class RepositoriesConfiguration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddScoped<ITransactionRepository, TransactionRepository>();
}