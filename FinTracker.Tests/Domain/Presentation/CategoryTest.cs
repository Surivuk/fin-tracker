using FinTracker.Domain.Presentation.Model;

namespace FinTracker.Tests.Domain.Presentation;

public class CategoryTest
{
    [Fact]
    public void ShouldBeUpdatable()
    {
        var newTitle = "New title";
        var newDescription = "New Description";

        var category = new Category("cat-id", "The Category", "This is the first category");

        category.ChangeTitle(newTitle);
        category.ChangeDescription(newDescription);

        Assert.Equal(newTitle, category.Title);
        Assert.Equal(newDescription, category.Description);
    }
}