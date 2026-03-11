namespace FinTracker.IDomain;

public interface IUnitOfWork
{
    public Task SaveChangesAsync();
}
