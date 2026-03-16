
using FinTracker.Presentation.Queries;
using FinTracker.Transaction.Commands;


internal static class TransactionsApi
{
    public static RouteGroupBuilder MapTransactions(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetCategories).WithName("GetTransactions");

        return group;
    }

    private async static Task<IResult> GetCategories(GetUserCategories query) => Results.Ok(new { Message = "ALL TRANSACTIONS" });

}