using FinTracker.Identity.Commands;
using FinTracker.Identity.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace FinTracker.Identity;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddIdentityModel(this IServiceCollection services)
    {
        return services
            .AddScoped<CreateUserBuilder>()
            .AddScoped<ChangeUserNameBuilder>()
            .AddScoped<ChangeUserCredentialsBuilder>()
            .AddScoped<DeleteUserBuilder>()

            .AddScoped<GetUser>();
    }
}