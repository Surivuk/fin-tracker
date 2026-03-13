using FinTracker.IDomain;

internal class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public Task SaveChangesAsync() => context.SaveChangesAsync();
}