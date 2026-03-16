namespace FinTracker.Transaction.Gateway;

public record CategoryModel(string Id, string UserId);

public interface ICategoryRepository
{
    public void Save(CategoryModel category);
    public void Delete(string id);
}
