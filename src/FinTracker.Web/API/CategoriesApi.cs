using FinTracker.DomainCore;
using FinTracker.IDomain;
using FinTracker.Presentation.Commands;
using FinTracker.Presentation.Queries;
using FinTracker.Transaction.Commands;

internal static class CategoriesApi
{
    public static RouteGroupBuilder MapCategories(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetCategories).WithName("GetCategories");
        group.MapPost("/create-category", CreateCategory).WithName("CreateCategory");

        return group;
    }

    private async static Task<IResult> GetCategories(GetUserCategories query) => Results.Json(await query.Execute());

    private async static Task<IResult> CreateCategory(
        NewCategoryData data,
        IUserIdProvider idProvider,
        DomainCommandExecutor executor,
        CreateCategoryBuilder categoryBuilder,
        CreatePresentationCategoryBuilder presentationBuilder)
    {
        var categoryId = EntityId.New.ToString();
        var userId = idProvider.GetUserId();

        var categoryResult = categoryBuilder.TryWith(new(categoryId, userId));
        var presentationResult = presentationBuilder.TryWith(new(categoryId, userId, data.Title, data.Description, data.Color));

        if (categoryResult.IsFailure) return Results.BadRequest();
        if (presentationResult.IsFailure) return Results.BadRequest();

        await executor.ExecuteAsync([categoryResult.Command!, presentationResult.Command!]);

        return Results.Created();
    }
}