namespace FinTracker.IDomain;

public interface IDomainHandlerRegistry
{
    public IReadOnlyDictionary<Type, HashSet<Type>> HandlerRegistrations { get; }
}
