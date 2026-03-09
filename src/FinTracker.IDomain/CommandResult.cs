namespace FinTracker.IDomain;

public class DomainCommandBuilderError(Type commandType, Exception exception) :
    Exception($"Failed to build a command \"{commandType.Name}\" - {exception.Message}");

public record DomainCommandResult
{
    public IDomainCommand? Command { get; private init; }
    public DomainCommandBuilderError? Error { get; private init; }
    public bool IsSuccess => Error is null;
    public bool IsFailure => Error is not null;

    public DomainCommandResult(IDomainCommand command) => Command = command;
    public DomainCommandResult(Type commandType, Exception error) => Error = new(commandType, error);
}
