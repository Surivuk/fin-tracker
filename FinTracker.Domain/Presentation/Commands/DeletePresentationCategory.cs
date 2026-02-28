using FinTracker.Domain.Abstractions;
using FinTracker.Domain.Presentation.Abstractions;
using FinTracker.Domain.Presentation.Model;

namespace FinTracker.Domain.Presentation.Commands;

public readonly record struct DeletePresentationCategoryData(EntityId Id);

public class DeletePresentationCategoryBuilder(ICategoryRepository repository) : IDomainCommandBuilder<DeletePresentationCategoryData>
{
    public IDomainCommand With(DeletePresentationCategoryData data) => new DeletePresentationCategory(repository, data);
}

public class DeletePresentationCategory(ICategoryRepository repository, DeletePresentationCategoryData data) : IDomainCommand
{
    public async Task Execute()
    {
        repository.Delete(data.Id);
    }
}