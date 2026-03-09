
namespace FinTracker.Transaction.Commands;

// public readonly record struct CreateDefaultCategoryRequestData(UserId UserId) : IDomainCommandData;

// public class CreateDefaultCategory(ICategoryRepository repository, IDomainBus domainBus) : IDomainCommand<CreateDefaultCategoryRequestData>
// {
//     public async Task Execute(CreateDefaultCategoryRequestData requestData)
//     {
//         var defaultCategory = new Category(CategoryId.New, requestData.UserId);

//         repository.Save(defaultCategory);

//         await domainBus.Emit<DefaultCategoryCreated>(new(defaultCategory.Id.ToString(), defaultCategory.UserId.ToString()));
//     }
// }