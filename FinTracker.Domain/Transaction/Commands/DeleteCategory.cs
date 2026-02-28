using FinTracker.Domain.Abstractions;
using FinTracker.Domain.Transaction.Abstractions;
using FinTracker.Domain.Transaction.Model;

namespace FinTracker.Domain.Transaction.Commands;

public readonly record struct DeleteCategoryData(CategoryId CategoryId);

public class DeleteCategoryBuilder(ICategoryRepository repository) : IDomainCommandBuilder<DeleteCategoryData>
{
    public IDomainCommand With(DeleteCategoryData data) => new DeleteCategory(repository, data);
}

public class DeleteCategory(ICategoryRepository repository, DeleteCategoryData data) : IDomainCommand
{
    public async Task Execute()
    {
        repository.Delete(data.CategoryId);
    }
}