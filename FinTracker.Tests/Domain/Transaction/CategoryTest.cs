using FinTracker.Domain.Transaction.Model;

namespace FinTracker.Tests.Domain.Transaction;

public class CategoryTest
{
    [Fact]
    public void ValidCategory()
    {
        var id = CategoryId.New;
        var t = new Category(id, UserId.From("c7134713-3768-493b-8845-46658e1dbe29"));

        Assert.Equal(id, t.Id);
    }
}