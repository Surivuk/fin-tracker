using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

internal class RepositoryException(string message) : Exception(message);

internal class Repository<Aggregate, AggregateId>(
    DbSet<Aggregate> set,
    IQueryable<Aggregate> queryable,
    Expression<Func<Aggregate, AggregateId>> keySelector,
    Func<AggregateId, Aggregate> createStub,
    Func<Aggregate, EntityEntry<Aggregate>> getTrackingStatus
) where Aggregate : class
{
    public async Task<Aggregate> Find(AggregateId id)
    {
        var category = await queryable.FirstAsync(BuildKeyExpression(id)) ?? throw new RepositoryException($"{typeof(Aggregate).Name} not found! Id: \"{id}\"");
        return category;
    }

    public void Save(Aggregate category)
    {
        var entry = getTrackingStatus(category);

        if (entry.State == EntityState.Detached)
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



