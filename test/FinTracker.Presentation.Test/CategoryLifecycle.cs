using FinTracker.Presentation.Commands;
using FinTracker.Presentation.Queries;
using FinTracker.TestKit;

namespace FinTracker.Presentation.Test;

public sealed class CategoryLifecycle : IDisposable
{
    private static readonly string _userId = "15a4b939-a42f-41bf-94da-4b6b6b91f25f";
    private readonly TestScope _scope = TestSetup.CreateScope(_userId);

    public void Dispose() => _scope.Dispose();
    private readonly CreatePresentationCategoryRequest _categoryRequest =
        new("08ef42fb-159a-4626-938e-6e6767a8f4ed", _userId, "Food", "This is a food category.", "#CCC");



    [Fact]
    public async Task Category_should_be_created()
    {
        var cmd = _scope.GetService<CreatePresentationCategoryBuilder>().TryWith(_categoryRequest).Command!;
        var query = _scope.GetService<GetUserCategory>();

        await cmd.Execute();
        var category = await query.Execute(_categoryRequest.Id);

        Assert.Equal(_categoryRequest.Id, category.Id);
    }

    [Fact]
    public async Task Category_should_be_deleted()
    {
        var createCmd = _scope.GetService<CreatePresentationCategoryBuilder>().TryWith(_categoryRequest).Command!;
        var deleteCmd = _scope.GetService<DeletePresentationCategoryBuilder>().TryWith(new(_categoryRequest.Id)).Command!;
        var query = _scope.GetService<GetUserCategory>();

        await createCmd.Execute();
        await deleteCmd.Execute();

        await Assert.ThrowsAnyAsync<Exception>(() => query.Execute(_categoryRequest.Id));
    }

    [Fact]
    public async Task Category_information_should_be_changed()
    {
        var changeRequest = new ChangeCategoryInformationRequest(_categoryRequest.Id, "New title", "New description");
        var createCmd = _scope.GetService<CreatePresentationCategoryBuilder>().TryWith(_categoryRequest).Command!;
        var changeCmd = _scope.GetService<ChangeCategoryInformationBuilder>().TryWith(changeRequest).Command!;
        var query = _scope.GetService<GetUserCategory>();

        await createCmd.Execute();
        await changeCmd.Execute();
        var updatedCategory = await query.Execute(_categoryRequest.Id);

        Assert.Equal(changeRequest.Title, updatedCategory.Title);
        Assert.Equal(changeRequest.Description, updatedCategory.Description);
    }

    [Fact]
    public async Task Category_appearance_should_be_changed()
    {
        var changeRequest = new ChangeCategoryAppearanceRequest(_categoryRequest.Id, "#FAFAFA");
        var createCmd = _scope.GetService<CreatePresentationCategoryBuilder>().TryWith(_categoryRequest).Command!;
        var changeCmd = _scope.GetService<ChangeCategoryAppearanceBuilder>().TryWith(changeRequest).Command!;
        var query = _scope.GetService<GetUserCategory>();

        await createCmd.Execute();
        await changeCmd.Execute();
        var updatedCategory = await query.Execute(_categoryRequest.Id);

        Assert.Equal(changeRequest.Color, updatedCategory.Color);
    }
}
