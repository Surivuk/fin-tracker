using FinTracker.IDomain;
using FinTracker.Transaction.Gateway;

internal static class CategoryConversions
{
    internal static Category ToDataModel(this CategoryModel model)
    {
        var id = EntityId.TryParse(model.Id);
        var userId = EntityId.TryParse(model.UserId);

        if (id.IsFailure) throw id.Error!;
        if (userId.IsFailure) throw userId.Error!;

        return new(id.Value, userId.Value);
    }

    internal static CategoryModel ToModel(this Category e) => new(e.Id.Value, e.UserId.Value);
}
