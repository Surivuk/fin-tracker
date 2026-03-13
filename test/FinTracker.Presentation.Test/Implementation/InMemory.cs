using FinTracker.Presentation.Gateway;

internal class InMemory
{
    public Dictionary<string, CategoryModel> Categories { get; } = [];
}
