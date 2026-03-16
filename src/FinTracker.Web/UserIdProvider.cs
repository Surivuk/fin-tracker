using System.Security.Claims;

internal interface IUserIdProvider
{
    public string GetUserId();
}

internal class UserIdProvider(IServiceProvider provider) : IUserIdProvider
{
    public string GetUserId()
    {
        var ctx = provider.GetRequiredService<IHttpContextAccessor>().HttpContext;
        var userId = (ctx?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value) ?? throw new Exception("Fail to get user id for JWT token!!!");
        return userId;
    }
}