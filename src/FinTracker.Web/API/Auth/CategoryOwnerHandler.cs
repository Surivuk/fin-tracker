using System.Security.Claims;
using FinTracker.Transaction.Gateway;
using Microsoft.AspNetCore.Authorization;

internal class CategoryOwnerRequirement : IAuthorizationRequirement { }

internal class CategoryOwnerHandler(IHttpContextAccessor httpContextAccessor, ICategoryOwnership categoryOwnership) : AuthorizationHandler<CategoryOwnerRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CategoryOwnerRequirement requirement)
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var categoryId = httpContextAccessor.HttpContext?.Request.RouteValues["id"]?.ToString();

        if (categoryId is null)
        {
            context.Succeed(requirement);
            return;
        }

        if (userId is null)
        {
            context.Fail();
            return;
        }

        var isAllowedResources = await categoryOwnership.IsMyCategory(userId, categoryId);

        if (isAllowedResources)
            context.Succeed(requirement);
        else
            context.Fail();
    }
}