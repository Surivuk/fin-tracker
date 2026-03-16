using Microsoft.EntityFrameworkCore;
using FinTracker.Transaction.Gateway;

internal class TransactionSchema(AppDbContext context)
{
    public Repository<CategoryModel, string> Categories => new(
        context.Set<CategoryModel>(),
        context.Set<CategoryModel>(),
        c => c.Id,
        id => new(id, string.Empty),
        c => TrackingStatusProcessor.ProcessForUpdate(context.ChangeTracker.Entries<CategoryModel>().FirstOrDefault(e => e.Entity.Id == c.Id))
    );
    public IQueryable<CategoryModel> CategoryQuery => context.Set<CategoryModel>().AsNoTracking();

    public Repository<TransactionModel, string> Transactions => new(
        context.Set<TransactionModel>(),
        context.Set<TransactionModel>(),
        c => c.Id,
        id => new(id, string.Empty, default, string.Empty, string.Empty),
        c => TrackingStatusProcessor.ProcessForUpdate(context.ChangeTracker.Entries<TransactionModel>().FirstOrDefault(e => e.Entity.Id == c.Id))
    );
    public IQueryable<TransactionModel> TransactionQuery => context.Set<TransactionModel>().AsNoTracking();
}

internal static class TransactionSchemaExtension
{
    public static ModelBuilder CreateTransactionSchema(this ModelBuilder modelBuilder)
    {
        string Scheme = "transaction";

        return modelBuilder
            .Entity<CategoryModel>(entity =>
            {
                entity.ToTable("Categories", Scheme);

                entity.HasKey(e => e.Id).HasName("PK_Transaction_Categories");
                entity.Property(e => e.Id);

                entity.Property(e => e.UserId).IsRequired();
            })
            .Entity<TransactionModel>(entity =>
            {
                entity.ToTable("Transactions", Scheme);

                entity.HasKey(e => e.Id).HasName("PK_Transaction_Transactions");
                entity.Property(e => e.Id);

                entity.Property(e => e.CategoryId).IsRequired();
                entity.HasOne<CategoryModel>().WithMany().HasForeignKey(c => c.CategoryId);

                entity.Property(e => e.MoneyAmount).IsRequired();
                entity.Property(e => e.MoneyCurrency).IsRequired();
                entity.Property(e => e.Type).IsRequired();
            });
    }
}