namespace FinTracker.Tests.App;

class ExecutionChecker
{
    private readonly Dictionary<Type, int> _logs = [];

    public void MarkAsExecuted(Type executorType)
    {
        if (_logs.TryGetValue(executorType, out var logCount))
            _logs[executorType] = ++logCount;
        else
            _logs.Add(executorType, 1);
    }

    public bool IsExecuted<T>()
    {
        var objectType = typeof(T);
        return _logs.ContainsKey(objectType);
    }

    public bool IsExecuted<T>(int executionCount)
    {
        var objectType = typeof(T);
        if (!_logs.TryGetValue(objectType, out var logCount)) return false;
        return executionCount == logCount;
    }
}
