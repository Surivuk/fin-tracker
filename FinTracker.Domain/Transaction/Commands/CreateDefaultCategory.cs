using FinTracker.Domain.Abstractions;
using FinTracker.Domain.Transaction.Abstractions;
using FinTracker.Domain.Transaction.Events;
using FinTracker.Domain.Transaction.Model;

namespace FinTracker.Domain.Transaction.Commands;

public readonly record struct CreateDefaultCategoryRequestData(UserId UserId) : IDomainCommandRequestData;

public class CreateDefaultCategory(ICategoryRepository repository, IDomainBus domainBus) : IDomainCommand<CreateDefaultCategoryRequestData>
{
    public async Task Execute(CreateDefaultCategoryRequestData requestData)
    {
        var defaultCategory = new Category(CategoryId.New, requestData.UserId);

        repository.Save(defaultCategory);

        await domainBus.Emit<DefaultCategoryCreated>(new(defaultCategory.Id.ToString(), defaultCategory.UserId.ToString()));
    }
}