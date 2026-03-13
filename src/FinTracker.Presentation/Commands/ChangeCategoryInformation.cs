using FinTracker.IDomain;
using FinTracker.Presentation.Gateway;

namespace FinTracker.Presentation.Commands;

public readonly record struct ChangeCategoryInformationRequest(string Id, string Title, string? Description);

public class ChangeCategoryInformationBuilder(ICategoryRepository repository) : IDomainCommandBuilder<ChangeCategoryInformationRequest>
{
    public DomainCommandResult TryWith(ChangeCategoryInformationRequest request)
    {
        var id = EntityId.TryParse(request.Id);
        var information = EntityInformation.New(request.Title, request.Description is null ? string.Empty : request.Description);

        if (id.IsFailure) return new(typeof(ChangeCategoryInformation), id.Error!);
        if (information.IsFailure) return new(typeof(ChangeCategoryInformation), information.Error!);

        return new(new ChangeCategoryInformation(repository, new(id.Value, information.Value)));
    }
}

internal readonly record struct ChangeCategoryInformationData(EntityId Id, EntityInformation Information);

public class ChangeCategoryInformation : IDomainCommand
{
    private readonly ICategoryRepository repository;
    private readonly ChangeCategoryInformationData data;

    internal ChangeCategoryInformation(ICategoryRepository repository, ChangeCategoryInformationData data)
    {
        this.repository = repository;
        this.data = data;
    }

    public async Task Execute()
    {
        var category = (await repository.GetCategory(data.Id.ToString())).ToEntity();

        category.ChangeInformation(data.Information);

        repository.Save(category.ToModel());
    }
}