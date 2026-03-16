using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

internal class RepositoryException(string message) : Exception(message);

internal static class TrackingStatusProcessor
{
    public static bool PrepareForUpdate<T>(AppDbContext context, Func<T, bool> isSame) where T : class
    {
        var existing = context.ChangeTracker.Entries<T>().FirstOrDefault(e => isSame(e.Entity));

        if (existing != null)
        {
            existing.State = EntityState.Detached;
            return false;
        }

        return true;
    }
}

internal class Repository<Aggregate, AggregateId>(
    DbSet<Aggregate> set,
    IQueryable<Aggregate> queryable,
    Expression<Func<Aggregate, AggregateId>> keySelector,
    Func<AggregateId, Aggregate> createStub,
    Func<Aggregate, bool> processTrackingStatus
) where Aggregate : class
{
    public async Task<Aggregate> Find(AggregateId id) => await queryable
        .FirstAsync(BuildKeyExpression(id)) ?? throw new RepositoryException($"{typeof(Aggregate).Name} not found! Id: \"{id}\"");

    public void Save(Aggregate category)
    {
        var isNew = processTrackingStatus(category);

        if (isNew)
            set.Add(category);
        else
            set.Update(category);
    }

    public void Delete(AggregateId id)
    {
        var category = set.Local.FirstOrDefault(BuildKeyExpression(id).Compile());

        category ??= createStub(id);

        set.Remove(category);
    }

    private Expression<Func<Aggregate, bool>> BuildKeyExpression(AggregateId id)
    {
        var match = Expression.Equal(keySelector.Body, Expression.Constant(id));
        return Expression.Lambda<Func<Aggregate, bool>>(match, keySelector.Parameters);
    }
}



