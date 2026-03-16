using FinTracker.IDomain;
using FinTracker.Transaction.Events;
using FinTracker.Transaction.Gateway;

namespace FinTracker.Transaction.Commands;

public readonly record struct CreateCategoryRequest(string CategoryId, string UserId);

public class CreateCategoryBuilder(ICategoryRepository repository, IDomainEventOutbox outbox) : IDomainCommandBuilder<CreateCategoryRequest>
{
    public DomainCommandResult TryWith(CreateCategoryRequest data)
    {
        var categoryId = EntityId.TryParse(data.CategoryId);
        var userId = EntityId.TryParse(data.UserId);

        if (categoryId.IsFailure) return new(typeof(CreateCategory), categoryId.Error!);
        if (userId.IsFailure) return new(typeof(CreateCategory), userId.Error!);

        return new(new CreateCategory(repository, outbox, new(categoryId.Value, userId.Value)));
    }
}

internal readonly record struct CreateCategoryData(EntityId CategoryId, EntityId UserId);

public class CreateCategory : IDomainCommand
{
    private readonly ICategoryRepository repository;
    private readonly IDomainEventOutbox outbox;
    private readonly CreateCategoryData data;

    internal CreateCategory(ICategoryRepository repository, IDomainEventOutbox outbox, CreateCategoryData data)
    {
        this.repository = repository;
        this.outbox = outbox;
        this.data = data;
    }

    public async Task Execute()
    {
        repository.Save(new(data.CategoryId.Value, data.UserId.Value));
        outbox.Add(new CategoryCreated(data.CategoryId.Value, data.UserId.Value));
    }
}