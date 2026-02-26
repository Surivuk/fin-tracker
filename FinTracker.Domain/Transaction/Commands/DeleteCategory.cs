using FinTracker.Domain.Abstractions;
using FinTracker.Domain.Transaction.Abstractions;
using FinTracker.Domain.Transaction.Model;

namespace FinTracker.Domain.Transaction.Commands;

public readonly record struct DeleteCategoryRequestData(CategoryId CategoryId) : IDomainCommandRequestData;

public class DeleteCategory(ICategoryRepository repository) : IDomainCommand<DeleteCategoryRequestData>
{
    public async Task Execute(DeleteCategoryRequestData requestData)
    {
        repository.Delete(requestData.CategoryId);
    }
}