namespace FinTracker.Persistence.Context;

using FinTracker.Domain.Transaction.Model;
using Microsoft.EntityFrameworkCore;

using TransactionCategory = Domain.Transaction.Model.Category;
using TransactionCategoryId = Domain.Transaction.Model.CategoryId;

using TransactionTransaction = Domain.Transaction.Model.Transaction;
using TransactionTransactionId = Domain.Transaction.Model.TransactionId;

using PresentationCategory = Domain.Presentation.Model.Category;
using PresentationCategoryId = Domain.Presentation.Model.EntityId;
using FinTracker.Domain.Presentation.Model;

public class AppDbContext : DbContext
{
    public Repository<TransactionCategory, TransactionCategoryId> TransactionCategories => new(
        Set<TransactionCategory>(),
        Set<TransactionCategory>(),
        c => c.Id,
        id => new TransactionCategory(id, default),
        c => Entry(c)
    );

    public Repository<TransactionTransaction, TransactionTransactionId> TransactionTransactions => new(
        Set<TransactionTransaction>(),
        Set<TransactionTransaction>(),
        c => c.Id,
        id => new TransactionTransaction(id, default, Money.New(1, Currency.EUR), TransactionType.Income),
        c => Entry(c)
    );

    // public DbSet<Domain.Presentation.Model.Category> PresentationCategories { get; set; }
    public Repository<PresentationCategory, PresentationCategoryId> PresentationCategories => new(
        Set<PresentationCategory>(),
        Set<PresentationCategory>(),
        c => c.Id,
        id => new PresentationCategory(id, default, EntityInformation.Default, CategoryAppearance.Default),
        c => Entry(c)
    );

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql(@"Host=localhost;Username=admin;Password=admin;Database=fin_tracker");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.BuildTransactionScheme();
        modelBuilder.BuildPresentationScheme();
    }
}