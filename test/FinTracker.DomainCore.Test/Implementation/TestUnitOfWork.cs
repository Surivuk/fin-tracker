using FinTracker.IDomain;

internal class TestUnitOfWork : IUnitOfWork
{
    private readonly List<ExecutionUnit> units = [];
    public ExecutionUnit CurrentUnit { get; private set; } = new();
    public int NumberOfUnits => units.Count;

    public async Task SaveChangesAsync()
    {
        units.Add(CurrentUnit);
        CurrentUnit = new ExecutionUnit();
    }

    public IReadOnlyList<string> GetExecutionUnit(int number)
    {
        if (units.Count <= number) throw new Exception($"The number is bigger than number of executed units. Number ${number}");
        
        foreach(var u in units)
                Console.WriteLine($"TEST - number: {number} - {string.Join(", ", u.Executions)}");

        return units[number].Executions;
    }
}