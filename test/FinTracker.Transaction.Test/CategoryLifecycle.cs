using FinTracker.Transaction.Commands;
using FinTracker.Transaction.Events;
using FinTracker.Transaction.Queries;
using FinTracker.Transaction.Repository;

namespace FinTracker.Transaction.Test;

public sealed class CategoryLifecycle : IDisposable
{
    private readonly TestScope _scope;
    private readonly CreateCategoryRequest _validRequest = new("bffb83d4-4c1d-4816-9c9c-466ea804c8df", "fbcf2ef2-1a5b-4ddd-84c0-f8021baeb899");

    public CategoryLifecycle() => _scope = TestScope.New;

    public void Dispose() => _scope.Dispose();

    [Fact]
    public async Task Should_create_new_category()
    {
        var query = _scope.GetService<GetCategory>();
        var cmd = _scope.GetService<CreateCategoryBuilder>().TryWith(_validRequest).Command!;

        await cmd.Execute();
        var category = await query.Execute(_validRequest.CategoryId);

        Assert.Equal(new CategoryModel(_validRequest.CategoryId, _validRequest.UserId), category);
    }

    [Fact]
    public async Task New_category_should_emit_category_created_event()
    {
        var cmd = _scope.GetService<CreateCategoryBuilder>().TryWith(_validRequest).Command!;

        await cmd.Execute();

        Assert.True(_scope.DomainBus.IsEventEmitted<CategoryCreated>());
    }

    [Fact]
    public async Task Category_should_delete_category()
    {
        var query = _scope.GetService<GetCategory>();
        var createCmd = _scope.GetService<CreateCategoryBuilder>().TryWith(_validRequest).Command!;
        var deleteCmd = _scope.GetService<DeleteCategoryBuilder>().TryWith(new(_validRequest.CategoryId)).Command!;

        await createCmd.Execute();
        await deleteCmd.Execute();

        await Assert.ThrowsAsync<Exception>(() => query.Execute(_validRequest.CategoryId));
    }
}
