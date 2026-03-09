using FinTracker.Transaction.Commands;

namespace FinTracker.Transaction.Test;

public sealed class BuildingCreateCategory : IDisposable
{
    private readonly TestScope _scope;

    public BuildingCreateCategory() => _scope = TestScope.New;

    public void Dispose() => _scope.Dispose();

    [Fact]
    public async Task Should_succeed()
    {
        var request = new CreateCategoryRequest("bffb83d4-4c1d-4816-9c9c-466ea804c8df", "fbcf2ef2-1a5b-4ddd-84c0-f8021baeb899");
        var cmdResult = _scope.GetService<CreateCategoryBuilder>().TryWith(request);

        Assert.True(cmdResult.IsSuccess);
    }

    public static IEnumerable<object[]> InvalidRequests =>
    [
        [new CreateCategoryRequest("NOT-GUID", "fbcf2ef2-1a5b-4ddd-84c0-f8021baeb899")],
        [new CreateCategoryRequest("a1b2c3d4-4c1d-4816-9c9c-466ea804c8df", "NOT_GUID")],
    ];

    [Theory]
    [MemberData(nameof(InvalidRequests))]
    public async Task Should_fail(CreateCategoryRequest request)
    {
        var cmdResult = _scope.GetService<CreateCategoryBuilder>().TryWith(request);

        Assert.True(cmdResult.IsFailure);
    }
}
