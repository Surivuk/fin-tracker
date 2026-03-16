using FinTracker.Transaction.Queries;
using Microsoft.AspNetCore.Authorization;

internal class CategoryOwnerRequirement : IAuthorizationRequirement { }

internal class CategoryOwnerHandler(IServiceProvider provider, IHttpContextAccessor httpContextAccessor) : AuthorizationHandler<CategoryOwnerRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CategoryOwnerRequirement requirement)
    {
        var categoryId = httpContextAccessor.HttpContext?.Request.RouteValues["id"]?.ToString();

        if (categoryId is null)
        {
            context.Succeed(requirement);
            return;
        }

        var getUsersCategories = provider.GetRequiredService<GetUsersCategories>();

        var allowedResources = await getUsersCategories.Execute();

        if (allowedResources.Contains(categoryId))
            context.Succeed(requirement);
        else
            context.Fail();
    }
}