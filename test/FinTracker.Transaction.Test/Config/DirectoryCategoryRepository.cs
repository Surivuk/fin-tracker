using FinTracker.Transaction.Repository;

internal class DirectoryCategoryRepository : ICategoryRepository
{
    private readonly Dictionary<string, CategoryModel> memory = [];

    public async Task<CategoryModel> GetCategory(string id)
    {
        // Console.WriteLine($"TEST - GET - {memory.First()}");
        if (!memory.TryGetValue(id, out var result)) throw new Exception($"Not found category with an id - {id}");

        return result;
    }
    public async Task<IEnumerable<CategoryModel>> GetCategories() => memory.Values.AsEnumerable();

    public void Save(CategoryModel category) => memory.Add(category.Id, category);
    public void Delete(string id) => memory.Remove(id);

}