using FinTracker.Presentation.Gateway;

internal class CategoryRepository(InMemory memory) : ICategoryRepository
{
    private readonly Dictionary<string, CategoryModel> categories = memory.Categories;

    public async Task<CategoryModel> GetCategory(string id)
    {
        if (!categories.TryGetValue(id, out var category)) throw new Exception($"Not found category with id:${id}");
        return category;
    }
    public void Save(CategoryModel category)
    {
        if (!categories.TryAdd(category.Id, category))
            categories[category.Id] = category;
    }
    public void Delete(string id) => categories.Remove(id);


}