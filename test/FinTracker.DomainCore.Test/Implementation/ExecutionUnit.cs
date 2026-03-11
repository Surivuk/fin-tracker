internal class ExecutionUnit
{
    private readonly List<string> executions = [];

    public IReadOnlyList<string> Executions => executions.AsReadOnly();

    public void RecordExecution(string identifier) => executions.Add(identifier);
}