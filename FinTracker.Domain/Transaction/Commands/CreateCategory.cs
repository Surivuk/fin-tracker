using FinTracker.Domain.Abstractions;
using FinTracker.Domain.Transaction.Abstractions;
using FinTracker.Domain.Transaction.Model;

namespace FinTracker.Domain.Transaction.Commands;

public readonly record struct CreateCategoryRequestData(UserId UserId) : IDomainCommandRequestData;

public class CreateCategory(ICategoryRepository repository) : IDomainCommand<CreateCategoryRequestData>
{
    public async Task Execute(CreateCategoryRequestData requestData)
    {
        var newCategory = new Category(CategoryId.New, requestData.UserId);
        repository.Save(newCategory);
    }
}