using FinTracker.Identity.Gateway;
using FinTracker.IDomain;

namespace FinTracker.Identity.Commands;

public readonly record struct ChangeUserNameRequest(string Id, string FirstName, string LastName);

public class ChangeUserNameBuilder(IUserGateway gateway) : IDomainCommandBuilder<ChangeUserNameRequest>
{
    public DomainCommandResult TryWith(ChangeUserNameRequest request)
    {
        var id = EntityId.TryParse(request.Id);
        var name = Name.New(request.FirstName, request.LastName);

        if (id.IsFailure) return new(typeof(ChangeUserName), id.Error!);
        if (name.IsFailure) return new(typeof(ChangeUserName), name.Error!);

        return new(new ChangeUserName(gateway, new(id.Value, name.Value)));
    }
}

internal readonly record struct CreateUserNameData(EntityId Id, Name Name);

public class ChangeUserName : IDomainCommand
{
    private readonly IUserGateway gateway;
    private readonly CreateUserNameData data;

    internal ChangeUserName(IUserGateway repository, CreateUserNameData data)
    {
        this.gateway = repository;
        this.data = data;
    }

    public Task Execute() => gateway.ChangeNameAsync(data.Id.ToString(), data.Name.FirstName, data.Name.LastName);
}