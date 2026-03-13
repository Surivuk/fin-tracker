using Microsoft.EntityFrameworkCore;

internal class AppDbContext : DbContext
{
    public TransactionSchema TransactionSchema => new(this);
    public PresentationSchema PresentationSchema => new(this);

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql(@"Host=localhost;Username=admin;Password=admin;Database=fin_tracker");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.CreateTransactionSchema();
        modelBuilder.CreatePresentationSchema();
    }
}