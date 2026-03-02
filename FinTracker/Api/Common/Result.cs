namespace FinTracker.Api.Common;

public class Result<T>
{
    public T? Value { get; private init; }
    public Exception? Error { get; private init; }
    public bool IsSuccess => Error is null;
    public bool IsFailure => Error is not null;

    public static Result<T> Try(Func<T> func)
    {
        try
        {
            return new Result<T> { Value = func() };
        }
        catch (Exception ex)
        {
            return new Result<T> { Error = ex };
        }
    }
}
