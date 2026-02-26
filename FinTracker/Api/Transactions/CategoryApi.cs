using FinTracker.Command;
using FinTracker.Domain.Transaction.Commands;
using FinTracker.Domain.Transaction.Model;

namespace FinTracker.Api.Transactions;

using CreateCategoryCommand = CommandExecutor<CreateCategory, CreateCategoryRequestData>;
using DeleteCategoryCommand = CommandExecutor<DeleteCategory, DeleteCategoryRequestData>;

public static class CategoryApi
{
    public static void MapCategories(this RouteGroupBuilder group)
    {
        group.MapPost("/", CreateCategory).WithName("CreateCategory");
        group.MapPost("/{id}/delete-category", DeleteCategory).WithName("DeleteCategory");
    }

    private async static Task<IResult> CreateCategory(CreateCategoryCommand command)
    {
        var userId = UserId.From("f356d754-1638-4722-bd16-1ad4ae8376dc");
        await command.Execute(new(userId));

        return Results.Created();
    }
    private async static Task<IResult> DeleteCategory(string id, DeleteCategoryCommand command)
    {
        await command.Execute(new(CategoryId.Parse(id)));

        return Results.Ok();
    }
}