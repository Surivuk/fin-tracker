using Microsoft.Extensions.DependencyInjection;

namespace FinTracker.TestKit;

public class TestScope(IServiceScope scope)
{
    private readonly IServiceProvider _provider = scope.ServiceProvider;

    public static TestScope New(Func<IServiceCollection, IServiceCollection> AddServices) =>
        new(AddServices(new ServiceCollection()).BuildServiceProvider().CreateScope());

    public T GetService<T>() where T : notnull => _provider.GetRequiredService<T>();
    public void Dispose() => scope.Dispose();
}