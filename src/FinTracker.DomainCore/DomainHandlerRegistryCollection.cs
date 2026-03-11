using FinTracker.IDomain;

namespace FinTracker.DomainCore;

public class DomainHandlerRegistryCollection(IEnumerable<IDomainHandlerRegistry> registryList)
{
    private readonly Dictionary<Type, HashSet<Type>> _registrations = registryList
            .SelectMany(r => r.HandlerRegistrations)
            .GroupBy(kvp => kvp.Key)
            .ToDictionary(
                g => g.Key,
                g => g.SelectMany(kvp => kvp.Value).ToHashSet()
            );

    public IReadOnlyDictionary<Type, HashSet<Type>> Registrations => _registrations.AsReadOnly();
}
