namespace FinTracker.Api.Transactions;

public static class ApiErrors
{
    public static IResult BadRequest(string field, Exception error) => Results.BadRequest(new {
            Title = "Bad Request",
            Status = 400,
            Error = new Dictionary<string, string> { { field, error.Message } }
        });
}
