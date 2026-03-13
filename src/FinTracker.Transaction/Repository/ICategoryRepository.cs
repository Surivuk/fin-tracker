namespace FinTracker.Transaction.Repository;

public record CategoryModel(string Id, string UserId);

public interface ICategoryRepository
{
    public void Save(CategoryModel category);
    public void Delete(string id);
}
