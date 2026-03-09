using FinTracker.IDomain;
using FinTracker.Transaction.Events;
using FinTracker.Transaction.Repository;

namespace FinTracker.Transaction.Commands;

public readonly record struct RemoveTransactionRequestData(string TransactionId);
public class RemoveTransactionBuilder(ITransactionRepository repository, IDomainBus domainBus) : IDomainCommandBuilder<RecordTransactionRequestData>
{
    public DomainCommandResult TryWith(RecordTransactionRequestData data)
    {
        var id = EntityId.TryParse(data.TransactionId);

        if (id.IsFailure) return new(typeof(RecordTransaction), id.Error!);

        return new(new RemoveTransaction(repository, domainBus, new(id.Value)));
    }
}

internal readonly record struct RemoveTransactionData(EntityId TransactionId);
public class RemoveTransaction : IDomainCommand
{
    private readonly ITransactionRepository repository;
    private readonly IDomainBus domainBus;
    private readonly RemoveTransactionData data;

    internal RemoveTransaction(ITransactionRepository repository, IDomainBus domainBus, RemoveTransactionData data)
    {
        this.repository = repository;
        this.domainBus = domainBus;
        this.data = data;
    }

    public async Task Execute()
    {
        var id = data.TransactionId.Value;
        repository.Delete(id);
        await domainBus.Emit(new TransactionRemoved(id));
    }
}