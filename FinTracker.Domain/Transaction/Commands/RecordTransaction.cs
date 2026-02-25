using FinTracker.Domain.Abstractions;
using FinTracker.Domain.Transaction.Abstractions;
using FinTracker.Domain.Transaction.Events;
using FinTracker.Domain.Transaction.Model;

namespace FinTracker.Domain.Transaction.Commands;

public readonly record struct RecordTransactionRequestData(
    TransactionId TransactionId,
    CategoryId CategoryId,
    Money Money,
    TransactionType TransactionType
): IDomainCommandRequestData;

public class RecordTransaction(ITransactionRepository repository, IDomainBus domainBus) : IDomainCommand<RecordTransactionRequestData>
{
    public async Task Execute(RecordTransactionRequestData requestData)
    {
        var newTransaction = new Model.Transaction(
            requestData.TransactionId,
            requestData.CategoryId,
            requestData.Money,
            requestData.TransactionType
        );

        repository.Save(newTransaction);

        await domainBus.Emit(new TransactionRecorded(
            newTransaction.Id.ToString(),
            newTransaction.CategoryId.ToString(),
            newTransaction.Money.Amount,
            newTransaction.Money.Currency.ToString()
        ));
    }
}