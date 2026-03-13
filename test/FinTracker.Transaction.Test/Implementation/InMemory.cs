using FinTracker.Transaction.Repository;

internal class InMemory
{
    public Dictionary<string, CategoryModel> Categories { get; } = [];
}
