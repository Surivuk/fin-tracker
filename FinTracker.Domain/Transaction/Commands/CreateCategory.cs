using FinTracker.Domain.Abstractions;
using FinTracker.Domain.Transaction.Abstractions;
using FinTracker.Domain.Transaction.Model;

namespace FinTracker.Domain.Transaction.Commands;

public readonly record struct CreateCategoryData(CategoryId CategoryId, UserId UserId);

public class CreateCategoryBuilder(ICategoryRepository repository) : IDomainCommandBuilder<CreateCategoryData>
{
    public IDomainCommand With(CreateCategoryData data) => new CreateCategory(repository, data);
}

public class CreateCategory(ICategoryRepository repository, CreateCategoryData requestData) : IDomainCommand
{
    public async Task Execute()
    {
        var newCategory = new Category(requestData.CategoryId, requestData.UserId);
        repository.Save(newCategory);
    }
}