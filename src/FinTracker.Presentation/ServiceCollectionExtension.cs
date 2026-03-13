using FinTracker.Presentation.Commands;
using FinTracker.Presentation.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace FinTracker.Presentation;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPresentationModel(this IServiceCollection services)
    {
        return services
            .AddScoped<CreatePresentationCategoryBuilder>()
            .AddScoped<DeletePresentationCategoryBuilder>()
            .AddScoped<ChangeCategoryInformationBuilder>()
            .AddScoped<ChangeCategoryAppearanceBuilder>()

            .AddScoped<GetUserCategory>()
            .AddScoped<GetUserCategories>();
    }
}