using FinTracker.Transaction.Gateway;

internal class CategoryRepository(InMemory memory) : ICategoryRepository
{
    private readonly Dictionary<string, CategoryModel> categories = memory.Categories;

    // public async Task<CategoryModel> GetCategory(string id)
    // {
    //     // Console.WriteLine($"TEST - GET - {memory.First()}");
    //     if (!memory.TryGetValue(id, out var result)) throw new Exception($"Not found category with an id - {id}");

    //     return result;
    // }
    // public async Task<IEnumerable<CategoryModel>> GetCategories() => memory.Values.AsEnumerable();

    public void Save(CategoryModel category) => categories.Add(category.Id, category);
    public void Delete(string id) => categories.Remove(id);
}