using FinTracker.IDomain;

namespace FinTracker.DomainCore;

public class DomainCommandExecutorException(string message, IEnumerable<Exception> innerExceptions) : AggregateException(message, innerExceptions);

public class DomainCommandExecutor(IUnitOfWork unitOfWork, DomainEventOutbox outbox)
{
    private int ExecutionCounter { get; set; } = 0;

    public async Task ExecuteAsync(params IDomainCommand[] commands)
    {
        foreach (var command in commands)
            await command.Execute();

        await unitOfWork.SaveChangesAsync();

        ExecutionCounter = 0;
        await ProcessOutbox();
    }

    private async Task ProcessOutbox()
    {
        if (outbox.IsEmpty) return;
        if (ExecutionCounter >= 5) throw new Exception("Outbox execution is too deep, more then 5 cycles!");

        var currentBatch = outbox.NextBatch();


        foreach (var handlerFunction in currentBatch)
            await handlerFunction();

        await unitOfWork.SaveChangesAsync();

        ExecutionCounter++;
        await ProcessOutbox();
    }
}