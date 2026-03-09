using FinTracker.IDomain;
using FinTracker.Transaction.Events;
using FinTracker.Transaction.Repository;

namespace FinTracker.Transaction.Commands;

public readonly record struct CreateCategoryRequest(string CategoryId, string UserId);

public class CreateCategoryBuilder(ICategoryRepository repository, IDomainBus bus) : IDomainCommandBuilder<CreateCategoryRequest>
{
    public DomainCommandResult TryWith(CreateCategoryRequest data)
    {
        var categoryId = EntityId.TryParse(data.CategoryId);
        var userId = EntityId.TryParse(data.UserId);

        if (categoryId.IsFailure) return new(typeof(CreateCategory), categoryId.Error!);
        if (userId.IsFailure) return new(typeof(CreateCategory), userId.Error!);

        return new(new CreateCategory(repository, bus, new(categoryId.Value, userId.Value)));
    }
}

internal readonly record struct CreateCategoryData(EntityId CategoryId, EntityId UserId);

public class CreateCategory : IDomainCommand
{
    private readonly ICategoryRepository repository;
    private readonly IDomainBus bus;
    private readonly CreateCategoryData data;

    internal CreateCategory(ICategoryRepository repository, IDomainBus bus, CreateCategoryData data)
    {
        this.repository = repository;
        this.bus = bus;
        this.data = data;
    }

    public async Task Execute()
    {
        repository.Save(new(data.CategoryId.Value, data.UserId.Value));
        await bus.Emit(new CategoryCreated(data.CategoryId.Value, data.UserId.Value));
    }
}