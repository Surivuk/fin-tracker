using FinTracker.IDomain;
using FinTracker.Presentation.Gateway;

namespace FinTracker.Presentation.Commands;

public readonly record struct ChangeCategoryAppearanceRequest(string Id, string Color);

public class ChangeCategoryAppearanceBuilder(ICategoryRepository repository) : IDomainCommandBuilder<ChangeCategoryAppearanceRequest>
{
    public DomainCommandResult TryWith(ChangeCategoryAppearanceRequest request)
    {
        var id = EntityId.TryParse(request.Id);
        var color = HexColor.TryParse(request.Color);

        if (id.IsFailure) return new(typeof(ChangeCategoryAppearance), id.Error!);
        if (color.IsFailure) return new(typeof(ChangeCategoryAppearance), color.Error!);

        ChangeCategoryAppearanceData data = new(id.Value, new(color.Value));

        return new(new ChangeCategoryAppearance(repository, data));
    }
}

internal readonly record struct ChangeCategoryAppearanceData(EntityId Id, CategoryAppearance Appearance);

public class ChangeCategoryAppearance : IDomainCommand
{
    private readonly ICategoryRepository repository;
    private readonly ChangeCategoryAppearanceData data;

    internal ChangeCategoryAppearance(ICategoryRepository repository, ChangeCategoryAppearanceData data)
    {
        this.repository = repository;
        this.data = data;
    }

    public async Task Execute()
    {
        var category = (await repository.GetCategory(data.Id.ToString())).ToEntity();

        category.ChangeAppearance(data.Appearance);

        repository.Save(category.ToModel());
    }
}