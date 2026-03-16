using FinTracker.Presentation.Queries;
using FinTracker.Transaction.Commands;


internal static class TransactionApi
{
    public static RouteGroupBuilder MapTransaction(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetCategories).WithName("GetTransaction");

        return group;
    }

    private async static Task<IResult> GetCategories(string transactionId) => Results.Ok(new { Message = $"ONE TRANSACTION - {transactionId}" });

}