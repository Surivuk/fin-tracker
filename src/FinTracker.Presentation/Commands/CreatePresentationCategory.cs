using FinTracker.IDomain;
using FinTracker.Presentation.Gateway;

namespace FinTracker.Presentation.Commands;

public readonly record struct CreatePresentationCategoryRequest(string Id, string UserId, string Title, string? Description, string Color);

public class CreatePresentationCategoryBuilder(ICategoryRepository repository) : IDomainCommandBuilder<CreatePresentationCategoryRequest>
{
    public DomainCommandResult TryWith(CreatePresentationCategoryRequest request)
    {
        var id = EntityId.TryParse(request.Id);
        var userId = EntityId.TryParse(request.UserId);
        var information = EntityInformation.New(request.Title, request.Description is null ? string.Empty : request.Description);
        var color = HexColor.TryParse(request.Color);

        if (id.IsFailure) return new(typeof(CreatePresentationCategory), id.Error!);
        if (id.IsFailure) return new(typeof(CreatePresentationCategory), userId.Error!);
        if (information.IsFailure) return new(typeof(CreatePresentationCategory), information.Error!);
        if (color.IsFailure) return new(typeof(CreatePresentationCategory), color.Error!);

        CreatePresentationCategoryData data = new(id.Value, userId.Value, information.Value, new(color.Value));

        return new(new CreatePresentationCategory(repository, data));
    }
}

internal readonly record struct CreatePresentationCategoryData(EntityId Id, EntityId UserId, EntityInformation Information, CategoryAppearance Appearance);

public class CreatePresentationCategory : IDomainCommand
{
    private readonly ICategoryRepository repository;
    private readonly CreatePresentationCategoryData data;

    internal CreatePresentationCategory(ICategoryRepository repository, CreatePresentationCategoryData data)
    {
        this.repository = repository;
        this.data = data;
    }

    public async Task Execute() => repository.Save(new Category(data.Id, data.UserId, data.Information, data.Appearance).ToModel());
}