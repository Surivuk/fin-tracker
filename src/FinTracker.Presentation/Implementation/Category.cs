using FinTracker.IDomain;
using FinTracker.Presentation.Gateway;

internal record CategoryAppearance(HexColor Color);

internal class Category(EntityId id, EntityId userId, EntityInformation information, CategoryAppearance appearance)
{
    public EntityId Id { get; private init; } = id;

    public EntityId UserId { get; private init; } = userId;

    public EntityInformation Information { get; private set; } = information;

    public CategoryAppearance Appearance { get; private set; } = appearance;

    public void ChangeInformation(EntityInformation newInformation) => Information = newInformation;

    public void ChangeAppearance(CategoryAppearance newAppearance) => Appearance = newAppearance;
}

internal static class CategoryConversions
{
    internal static CategoryModel ToModel(this Category category)
    {
        return new(
            category.Id.ToString(),
            category.UserId.ToString(),
            category.Information.Title,
            category.Information.Description,
            category.Appearance.Color.ToString()
        );
    }

    internal static Category ToEntity(this CategoryModel model)
    {
        var id = EntityId.TryParse(model.Id);
        var userId = EntityId.TryParse(model.UserId);
        var information = EntityInformation.New(model.Title, model.Description is null ? string.Empty : model.Description);
        var color = HexColor.TryParse(model.Color);

        if (id.IsFailure) throw id.Error!;
        if (id.IsFailure) throw userId.Error!;
        if (information.IsFailure) throw information.Error!;
        if (color.IsFailure) throw color.Error!;

        return new(id.Value, userId.Value, information.Value, new(color.Value));
    }
}