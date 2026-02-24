
using FinTracker.Domain.Presentation.Model;

namespace FinTracker.Tests.Domain.Presentation;

public class EntityIdText
{

    [Fact]
    public void ShouldBeValid()
    {
        var guidId = Guid.NewGuid();
        var id = EntityId.From(guidId.ToString());

        Assert.Equal(guidId.ToString(), id.Value);
    }

    [Fact]
    public void ShouldBeInvalid()
    {
        Assert.Throws<InvalidEntityId>(() => EntityId.From("no-guid-value"));
    }
}