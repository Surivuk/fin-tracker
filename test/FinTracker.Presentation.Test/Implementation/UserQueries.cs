using System.Collections.Immutable;
using FinTracker.Presentation.Gateway;

internal class UserQueries(InMemory memory, string UserId) : IUserQueries
{
    public async Task<IReadOnlyList<UserCategoryModel>> GetUserCategories() =>
        memory.Categories.Values.Where(c => c.UserId == UserId).Select(c => new UserCategoryModel(c.Id, c.Title, c.Description, c.Color)).ToImmutableList();

    public async Task<UserCategoryModel> GetUserCategory(string categoryId)
    {
        if (!memory.Categories.TryGetValue(categoryId, out var category)) throw new Exception($"Not found category with id:${categoryId}");
        if (category.UserId != UserId) throw new Exception($"Not found category with id:${categoryId}");

        return new(category.Id, category.Title, category.Description, category.Color);
    }
}