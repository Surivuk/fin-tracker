using FinTracker.Command;
using FinTracker.Domain.Presentation.Commands;
using FinTracker.Domain.Presentation.Model;
using FinTracker.Domain.Transaction.Commands;
using FinTracker.Domain.Transaction.Model;

namespace FinTracker.Api.Transactions;

readonly record struct NewCategoryData(
   string Title,
   string Description,
   string Color
);

public static class CategoryApi
{
    public static void MapCategories(this RouteGroupBuilder group)
    {
        group.MapPost("/", CreateCategory).WithName("CreateCategory");
        group.MapPost("/{id}/delete-category", DeleteCategory).WithName("DeleteCategory");
    }

    private async static Task<IResult> CreateCategory(
        NewCategoryData data,
        CreateCategoryBuilder createCategory,
        CreatePresentationCategoryBuilder createPresentationCategory,
        CommandExecutor executor)
    {
        var userId = UserId.From("f356d754-1638-4722-bd16-1ad4ae8376dc");
        var categoryId = CategoryId.New;

        var information = EntityInformation.New(data.Title, data.Description);
        var appearance = new CategoryAppearance(HexColor.From(data.Color));

        await executor.ExecuteAsync(
            createCategory.With(new(categoryId, userId)),
            createPresentationCategory.With(new(categoryId.ToEntityId(), userId.ToEntityId(), information, appearance))
        );

        return Results.Created();
    }
    private async static Task<IResult> DeleteCategory(
        string id,
        DeleteCategoryBuilder deleteCategory,
        DeletePresentationCategoryBuilder deletePresentationCategory,
        CommandExecutor executor)
    {
        var categoryId = CategoryId.Parse(id);

        await executor.ExecuteAsync(
            deleteCategory.With(new(categoryId)),
            deletePresentationCategory.With(new(categoryId.ToEntityId()))
        );

        return Results.NoContent();
    }

    private static EntityId ToEntityId(this CategoryId id) => EntityId.From(id.ToString());
    private static EntityId ToEntityId(this UserId id) => EntityId.From(id.ToString());
}