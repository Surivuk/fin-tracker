using FinTracker.Transaction.Commands;
using FinTracker.Transaction.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace FinTracker.Transaction;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddTransactionModel(this IServiceCollection services)
    {
        return services
            .AddScoped<CreateCategoryBuilder>()
            .AddScoped<DeleteCategoryBuilder>()
            .AddScoped<RecordTransactionBuilder>()
            .AddScoped<RemoveTransactionBuilder>()

            .AddScoped<GetUsersCategories>();
    }
}