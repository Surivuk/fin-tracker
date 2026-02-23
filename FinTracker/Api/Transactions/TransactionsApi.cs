using FinTracker.Command;
using FinTracker.Domain.Transaction.Commands;
using FinTracker.Domain.Transaction.Model;

namespace FinTracker.Api.Transactions;

using RecordTransactionCommand = CommandExecutor<RecordTransaction, RecordTransactionRequestData>;

public static class TransactionsApi
{
    public static void MapTransactions(this RouteGroupBuilder group)
    {
        group.MapPost("/", RecordTransaction).WithName("RecordTransaction");
    }

    private async static Task<IResult> RecordTransaction(RecordTransactionCommand command)
    {
        var transactionId = TransactionId.New;

        await command.Execute(new(
            transactionId,
            CategoryId.Parse("41e28d01-3f7e-4ccb-8dc0-e86e58fb0620"),
            Money.New(500, Currency.EUR),
            TransactionType.Income));

        return Results.Created();
    }
}