using FinTracker.TestKit;
using FinTracker.Transaction.Commands;
using FinTracker.Transaction.Queries;

namespace FinTracker.Transaction.Test;

public sealed class CategoryLifecycle : IDisposable
{
    private readonly TestScope _scope = TestSetup.CreateScope("fbcf2ef2-1a5b-4ddd-84c0-f8021baeb899");
    private readonly CreateCategoryRequest _validRequest = new("bffb83d4-4c1d-4816-9c9c-466ea804c8df", "fbcf2ef2-1a5b-4ddd-84c0-f8021baeb899");
    public void Dispose() => _scope.Dispose();

    [Fact]
    public async Task Should_create_new_category()
    {
        var query = _scope.GetService<GetUsersCategories>();
        var cmd = _scope.GetService<CreateCategoryBuilder>().TryWith(_validRequest).Command!;

        await cmd.Execute();
        var category = await query.Execute();

        Assert.Contains(_validRequest.CategoryId, category);
    }

    [Fact]
    public async Task Category_should_delete_category()
    {
        var query = _scope.GetService<GetUsersCategories>();
        var createCmd = _scope.GetService<CreateCategoryBuilder>().TryWith(_validRequest).Command!;
        var deleteCmd = _scope.GetService<DeleteCategoryBuilder>().TryWith(new(_validRequest.CategoryId)).Command!;

        await createCmd.Execute();
        await deleteCmd.Execute();
        var categories = await query.Execute();

        Assert.False(categories.Any());
    }
}
