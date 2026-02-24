using FinTracker.Domain.Presentation.Model;

namespace FinTracker.Tests.Domain.Presentation;

public class CategoryTest
{
    private readonly EntityId _categoryId = EntityId.From(Guid.NewGuid().ToString());
    private readonly EntityId _userId = EntityId.From(Guid.NewGuid().ToString());

    [Fact]
    public void ShouldChangeInformation()
    {
        var newInformation = EntityInformation.New("New title", "New Description");

        var category = new Category(_categoryId, _userId, EntityInformation.New("The Category", "This is the first category"), new(HexColor.Default));

        category.ChangeInformation(newInformation);

        Assert.Equal(newInformation, category.Information);
    }

    [Fact]
    public void ShouldChangeAppearance()
    {
        CategoryAppearance newAppearance = new(HexColor.From("#FAFAFA"));

        var category = new Category(_categoryId, _userId, EntityInformation.New("The Category", "This is the first category"), new(HexColor.Default));

        category.ChangeAppearance(newAppearance);

        Assert.Equal(newAppearance, category.Appearance);
    }
}