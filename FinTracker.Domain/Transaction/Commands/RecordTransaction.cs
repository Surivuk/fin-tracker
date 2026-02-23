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

public class RecordTransaction(ITransactionRepository Repository, IDomainBus DomainBus) : IDomainCommand<RecordTransactionRequestData>
{
    public async Task Execute(RecordTransactionRequestData requestData)
    {
        var newTransaction = new Model.Transaction(
            requestData.TransactionId,
            requestData.CategoryId,
            requestData.Money,
            requestData.TransactionType
        );

        Repository.Save(newTransaction);

        await DomainBus.Emit(new TransactionRecorded(
            newTransaction.Id.ToString(),
            newTransaction.CategoryId.ToString(),
            newTransaction.Money.Amount,
            newTransaction.Money.Currency.ToString()
        ));
    }
}