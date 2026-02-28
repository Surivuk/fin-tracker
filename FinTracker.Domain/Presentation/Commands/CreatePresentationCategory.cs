using FinTracker.Domain.Abstractions;
using FinTracker.Domain.Presentation.Abstractions;
using FinTracker.Domain.Presentation.Model;

namespace FinTracker.Domain.Presentation.Commands;

public readonly record struct CreatePresentationCategoryData(
    EntityId Id,
    EntityId UserId,
    EntityInformation Information,
    CategoryAppearance Appearance
);

public class CreatePresentationCategoryBuilder(ICategoryRepository repository) : IDomainCommandBuilder<CreatePresentationCategoryData>
{
    public IDomainCommand With(CreatePresentationCategoryData data) => new CreatePresentationCategory(repository, data);
}

public class CreatePresentationCategory(ICategoryRepository repository, CreatePresentationCategoryData data) : IDomainCommand
{
    public async Task Execute()
    {
        var newCategory = new Category(data.Id, data.UserId, data.Information, data.Appearance);
        repository.Save(newCategory);
    }

}