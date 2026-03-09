namespace FinTracker.IDomain;

public record Result<T>
{
    public T? Value { get; private init; }
    public Exception? Error { get; private init; }
    public bool IsSuccess => Error is null;
    public bool IsFailure => Error is not null;

    public Result(T value) => Value = value;
    public Result(Exception error) => Error = error;
}
