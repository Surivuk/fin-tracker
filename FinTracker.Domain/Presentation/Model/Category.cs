namespace FinTracker.Domain.Presentation.Model;

public readonly record struct CategoryAppearance(HexColor Color);

public class Category(EntityId id, EntityId userId, EntityInformation information, CategoryAppearance appearance)
{
    public EntityId Id { get; private init; } = id;

    public EntityId UserId { get; private init; } = userId;

    public EntityInformation Information { get; private set; } = information;

    public CategoryAppearance Appearance { get; private set; } = appearance;

    public void ChangeInformation(EntityInformation newInformation) => Information = newInformation;

    public void ChangeAppearance(CategoryAppearance newAppearance) => Appearance = newAppearance;
}