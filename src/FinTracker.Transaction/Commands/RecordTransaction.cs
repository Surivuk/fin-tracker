using FinTracker.IDomain;
using FinTracker.Transaction.Events;
using FinTracker.Transaction.Implementation;
using FinTracker.Transaction.Repository;

namespace FinTracker.Transaction.Commands;

public readonly record struct RecordTransactionRequestData(
    string TransactionId, string CategoryId, double MoneyAmount, string Currency, string TransactionType);
public class RecordTransactionBuilder(ITransactionRepository repository, IDomainEventOutbox outbox) : IDomainCommandBuilder<RecordTransactionRequestData>
{
    public DomainCommandResult TryWith(RecordTransactionRequestData data)
    {
        var id = EntityId.TryParse(data.TransactionId);
        var categoryId = EntityId.TryParse(data.CategoryId);
        var moneyAmount = MoneyAmount.TryParse(data.MoneyAmount);
        var moneyCurrency = Currency.TryParse(data.Currency);
        var type = TransactionType.TryParse(data.TransactionType);

        if (id.IsFailure) return new(typeof(RecordTransaction), id.Error!);
        if (categoryId.IsFailure) return new(typeof(RecordTransaction), categoryId.Error!);
        if (moneyAmount.IsFailure) return new(typeof(RecordTransaction), moneyAmount.Error!);
        if (moneyCurrency.IsFailure) return new(typeof(RecordTransaction), moneyCurrency.Error!);
        if (type.IsFailure) return new(typeof(RecordTransaction), type.Error!);

        RecordTransactionData requestData = new(id.Value, categoryId.Value, new(moneyAmount.Value, moneyCurrency.Value), type.Value);

        return new(new RecordTransaction(repository, outbox, requestData));
    }
}

internal readonly record struct RecordTransactionData(
        EntityId TransactionId, EntityId CategoryId, Money Money, TransactionType TransactionType);
public class RecordTransaction : IDomainCommand
{
    private readonly ITransactionRepository repository;
    private readonly IDomainEventOutbox outbox;
    private readonly RecordTransactionData data;

    internal RecordTransaction(ITransactionRepository repository, IDomainEventOutbox outbox, RecordTransactionData data)
    {
        this.repository = repository;
        this.outbox = outbox;
        this.data = data;
    }

    public async Task Execute()
    {
        var newTransaction = new TransactionEntity(
            data.TransactionId,
            data.CategoryId,
            data.Money,
            data.TransactionType
        );

        repository.Save(newTransaction.ToModel());

        outbox.Add(new TransactionRecorded(
            newTransaction.Id.Value,
            newTransaction.CategoryId.Value,
            newTransaction.Money.Amount.Value,
            newTransaction.Money.Currency.ToString()
        ));
    }
}