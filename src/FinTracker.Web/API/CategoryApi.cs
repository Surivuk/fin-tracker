using FinTracker.DomainCore;
using FinTracker.IDomain;
using FinTracker.Presentation.Commands;
using FinTracker.Presentation.Queries;
using FinTracker.Transaction.Commands;

readonly record struct NewCategoryData(
   string Title,
   string Description,
   string Color
);

readonly record struct ChangeInformationData(
   string Title,
   string Description
);
readonly record struct ChangeAppearanceData(
   string Color
);

internal static class CategoryApi
{
    public static RouteGroupBuilder MapCategory(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetCategory).WithName("GetCategory");
        group.MapPost("/change-information", ChangeInformation).WithName("ChangeCategoryInformation");
        group.MapPost("/change-appearance", ChangeAppearance).WithName("ChangeCategoryAppearance");
        group.MapPost("/delete-category", DeleteCategory).WithName("DeleteCategory");

        return group;
    }

    private async static Task<IResult> GetCategory(string id, GetUserCategory query) => Results.Json(await query.Execute(id));

    private async static Task<IResult> ChangeInformation(string id, ChangeInformationData data,
        ChangeCategoryInformationBuilder builder, DomainCommandExecutor executor)
    {
        var cmd = builder.TryWith(new(id, data.Title, data.Description));

        if (cmd.IsFailure) return Results.BadRequest();

        await executor.ExecuteAsync(cmd.Command!);

        return Results.NoContent();
    }

    private async static Task<IResult> ChangeAppearance(string id, ChangeAppearanceData data,
        ChangeCategoryAppearanceBuilder builder, DomainCommandExecutor executor)
    {
        var cmd = builder.TryWith(new(id, data.Color));

        if (cmd.IsFailure) return Results.BadRequest();

        await executor.ExecuteAsync(cmd.Command!);

        return Results.NoContent();
    }

    private async static Task<IResult> DeleteCategory(
        string id,
        DeleteCategoryBuilder deleteCategory,
        DeletePresentationCategoryBuilder deletePresentationCategory,
        DomainCommandExecutor executor)
    {
        var deleteCategoryResult = deleteCategory.TryWith(new(id));
        var deletePresentationResult = deletePresentationCategory.TryWith(new(id));

        if (deleteCategoryResult.IsFailure) return Results.BadRequest();
        if (deletePresentationResult.IsFailure) return Results.BadRequest();

        await executor.ExecuteAsync([deleteCategoryResult.Command!, deletePresentationResult.Command!]);

        return Results.NoContent();
    }

}