using FinTracker.Domain.Abstractions;
using FinTracker.Domain.Presentation.Abstractions;
using FinTracker.Domain.Transaction.Events;

namespace FinTracker.Domain.Presentation.Handlers;

public class CategoryCreatedHandler(ICategoryRepository repository) : IDomainEventHandler<CategoryCreated>
{
    public async Task Handle(CategoryCreated domainEvent)
    {
        // var newCategory = new Category

        // repository.Save()
    }
}