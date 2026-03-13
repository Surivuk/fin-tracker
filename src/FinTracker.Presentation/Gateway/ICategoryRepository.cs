
namespace FinTracker.Presentation.Gateway;

public record CategoryModel(string Id, string UserId, string Title, string? Description, string Color);

public interface ICategoryRepository
{
    public Task<CategoryModel> GetCategory(string id);
    public void Save(CategoryModel category);
    public void Delete(string id);
}