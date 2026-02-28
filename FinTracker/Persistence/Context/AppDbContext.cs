namespace FinTracker.Persistence.Context;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Domain.Transaction.Model.Category> TransactionCategories { get; set; }
    public DbSet<Domain.Transaction.Model.Transaction> TransactionTransactions { get; set; }


    public DbSet<Domain.Presentation.Model.Category> PresentationCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql(@"Host=localhost;Username=admin;Password=admin;Database=fin_tracker");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.BuildTransactionScheme();
        modelBuilder.BuildPresentationScheme();
    }
}