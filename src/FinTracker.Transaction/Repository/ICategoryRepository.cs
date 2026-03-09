namespace FinTracker.Transaction.Repository;

public record CategoryModel(string Id, string UserId);

public interface ICategoryRepository
{
    public Task<CategoryModel> GetCategory(string id);
    public Task<IEnumerable<CategoryModel>> GetCategories();
    public void Save(CategoryModel category);
    public void Delete(string id);
}
