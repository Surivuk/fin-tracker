using FinTracker.IDomain;
using FinTracker.Transaction.Events;
using FinTracker.Transaction.Repository;

namespace FinTracker.Transaction.Commands;

public readonly record struct DeleteCategoryRequest(string CategoryId);

public class DeleteCategoryBuilder(ICategoryRepository repository, IDomainBus domainBus) : IDomainCommandBuilder<DeleteCategoryRequest>
{
    public DomainCommandResult TryWith(DeleteCategoryRequest data)
    {
        var categoryId = EntityId.TryParse(data.CategoryId);

        if (categoryId.IsFailure) return new(typeof(DeleteCategory), categoryId.Error!);

        return new(new DeleteCategory(repository, domainBus, new(categoryId.Value)));
    }
}

internal readonly record struct DeleteCategoryData(EntityId CategoryId);
public class DeleteCategory : IDomainCommand
{
    private readonly ICategoryRepository repository;
    private readonly IDomainBus domainBus;
    private readonly DeleteCategoryData data;

    internal DeleteCategory(ICategoryRepository repository, IDomainBus domainBus, DeleteCategoryData data)
    {
        this.repository = repository;
        this.domainBus = domainBus;
        this.data = data;
    }

    public async Task Execute()
    {
        var id = data.CategoryId.Value;
        repository.Delete(id);
        await domainBus.Emit(new CategoryDeleted(id));
    }
}