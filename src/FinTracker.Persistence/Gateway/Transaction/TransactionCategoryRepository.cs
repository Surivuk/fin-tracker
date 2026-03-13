using FinTracker.Transaction.Repository;

internal class TransactionCategoryRepository(AppDbContext context) : ICategoryRepository
{
    private readonly Repository<CategoryModel, string> repository = context.TransactionSchema.Categories;

    public Task<CategoryModel> GetCategory(string id) => repository.Find(id);
    public void Save(CategoryModel category) => repository.Save(category);
    public void Delete(string id) => repository.Delete(id);
}