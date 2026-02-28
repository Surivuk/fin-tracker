using FinTracker.Domain.Presentation.Abstractions;
using FinTracker.Domain.Presentation.Commands;
using FinTracker.Persistence.Repository.Presentation;

namespace FinTracker.Configuration;

public static class PresentationContextConfiguration
{
    public static IServiceCollection AddPresentationContext(this IServiceCollection services) =>
        services
            .AddScoped<CreatePresentationCategoryBuilder>()
            .AddScoped<DeletePresentationCategoryBuilder>()

            .AddScoped<ICategoryRepository, CategoryRepository>();
}