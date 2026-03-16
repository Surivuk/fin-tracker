using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

internal static class ServiceCollectionExtension
{
    public static IServiceCollection AddApiAuth(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUserIdProvider, UserIdProvider>();
        services.AddScoped<IAuthorizationHandler, CategoryOwnerHandler>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.Authority = "http://localhost:8080/realms/Playground";
            options.Audience = "app-api";
            options.RequireHttpsMetadata = false; // set true in production
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,           // checks "iss" claim matches Authority
                ValidateAudience = true,         // checks "aud" claim matches Audience
                ValidateLifetime = true,         // checks "exp" claim, token not expired
                ValidateIssuerSigningKey = true, // verifies signature using Keycloak public key
                ClockSkew = TimeSpan.Zero        // no tolerance for expiry (default is 5 min)
            };
            AddJwtLogger(options, false);
        });

        services.AddAuthorizationBuilder().AddPolicy("CategoryOwner", policy => policy.Requirements.Add(new CategoryOwnerRequirement()));
        services.AddAuthorization();

        return services;
    }

    private static void AddJwtLogger(JwtBearerOptions options, bool shouldBeAdded)
    {
        if (!shouldBeAdded) return;

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token validated successfully");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                Console.WriteLine($"Challenge error: {context.Error}");
                Console.WriteLine($"Challenge description: {context.ErrorDescription}");
                return Task.CompletedTask;
            }
        };
    }
}