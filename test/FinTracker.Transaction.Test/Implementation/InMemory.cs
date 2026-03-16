using FinTracker.Transaction.Gateway;

internal class InMemory
{
    public Dictionary<string, CategoryModel> Categories { get; } = [];
}
