using FinTracker.Domain.Transaction.Model;

namespace FinTracker.Tests.Domain.Transaction;

public class CategoryTest
{
    [Fact]
    public void ValidCategory()
    {
        var id = CategoryId.New;
        var t = new Category(id, "user");

        Assert.Equal(id, t.Id);
    }
}