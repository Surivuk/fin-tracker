using FinTracker.IDomain;
using FinTracker.Transaction.Events;
using FinTracker.Transaction.Repository;

namespace FinTracker.Transaction.Commands;

public readonly record struct DeleteCategoryRequest(string CategoryId);

public class DeleteCategoryBuilder(ICategoryRepository repository, IDomainEventOutbox outbox) : IDomainCommandBuilder<DeleteCategoryRequest>
{
    public DomainCommandResult TryWith(DeleteCategoryRequest data)
    {
        var categoryId = EntityId.TryParse(data.CategoryId);

        if (categoryId.IsFailure) return new(typeof(DeleteCategory), categoryId.Error!);

        return new(new DeleteCategory(repository, outbox, new(categoryId.Value)));
    }
}

internal readonly record struct DeleteCategoryData(EntityId CategoryId);
public class DeleteCategory : IDomainCommand
{
    private readonly ICategoryRepository repository;
    private readonly IDomainEventOutbox outbox;
    private readonly DeleteCategoryData data;

    internal DeleteCategory(ICategoryRepository repository, IDomainEventOutbox outbox, DeleteCategoryData data)
    {
        this.repository = repository;
        this.outbox = outbox;
        this.data = data;
    }

    public async Task Execute()
    {
        var id = data.CategoryId.Value;
        repository.Delete(id);
        outbox.Add(new CategoryDeleted(id));
    }
}