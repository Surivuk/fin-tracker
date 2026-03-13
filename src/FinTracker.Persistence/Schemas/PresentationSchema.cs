using FinTracker.Presentation.Gateway;
using Microsoft.EntityFrameworkCore;

internal class PresentationSchema(AppDbContext context)
{
    public Repository<CategoryModel, string> Categories => new(
        context.Set<CategoryModel>(),
        context.Set<CategoryModel>(),
        c => c.Id,
        id => new(id, string.Empty, string.Empty, null, string.Empty),
        c => context.Entry(c)
    );
    public IQueryable<CategoryModel> CategoryQuery => context.Set<CategoryModel>().AsNoTracking();

    // public Repository<, string> TransactionTransactions => new(
    //     context.Set<TransactionModel>(),
    //     context.Set<TransactionModel>(),
    //     c => c.Id,
    //     id => new(id, string.Empty, default, string.Empty, string.Empty),
    //     c => context.Entry(c)
    // );
    // public IQueryable<TransactionModel> TransactionTransactionQueryable => context.Set<TransactionModel>().AsNoTracking();
}

internal static class PresentationSchemaExtension
{
    public static ModelBuilder CreatePresentationSchema(this ModelBuilder modelBuilder)
    {
        string Schema = "presentation";

        return modelBuilder.Entity<CategoryModel>(entity =>
        {
            entity.ToTable("Categories", Schema);

            entity.HasKey(e => e.Id).HasName("PK_Presentation_Categories"); ;
            entity.Property(e => e.Id);

            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Description);
            entity.Property(a => a.Color).IsRequired();
        });
    }
}