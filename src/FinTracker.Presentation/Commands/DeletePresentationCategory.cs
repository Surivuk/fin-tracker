using FinTracker.IDomain;
using FinTracker.Presentation.Gateway;

namespace FinTracker.Presentation.Commands;

public readonly record struct DeletePresentationCategoryRequest(string Id);

public class DeletePresentationCategoryBuilder(ICategoryRepository repository) : IDomainCommandBuilder<DeletePresentationCategoryRequest>
{
    public DomainCommandResult TryWith(DeletePresentationCategoryRequest request)
    {
        var id = EntityId.TryParse(request.Id);

        if (id.IsFailure) return new(typeof(DeletePresentationCategory), id.Error!);

        return new(new DeletePresentationCategory(repository, new(id.Value)));
    }
}

internal readonly record struct DeletePresentationCategoryData(EntityId Id);

public class DeletePresentationCategory : IDomainCommand
{
    private readonly ICategoryRepository repository;
    private readonly DeletePresentationCategoryData data;


    internal DeletePresentationCategory(ICategoryRepository repository, DeletePresentationCategoryData data)
    {
        this.repository = repository;
        this.data = data;
    }
    public async Task Execute() => repository.Delete(data.Id.ToString());
}