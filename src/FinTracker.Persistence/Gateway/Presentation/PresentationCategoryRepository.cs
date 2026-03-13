using FinTracker.Presentation.Gateway;

internal class PresentationCategoryRepository(AppDbContext context) : ICategoryRepository
{
    private readonly Repository<CategoryModel, string> repository = context.PresentationSchema.Categories;

    public Task<CategoryModel> GetCategory(string id) => repository.Find(id);
    public void Save(CategoryModel category) => repository.Save(category);
    public void Delete(string id) => repository.Delete(id);
}