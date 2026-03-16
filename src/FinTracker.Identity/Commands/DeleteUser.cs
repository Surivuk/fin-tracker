using FinTracker.Identity.Gateway;
using FinTracker.IDomain;

namespace FinTracker.Identity.Commands;

public readonly record struct DeleteUserRequest(string Id);

public class DeleteUserBuilder(IUserGateway gateway) : IDomainCommandBuilder<DeleteUserRequest>
{
    public DomainCommandResult TryWith(DeleteUserRequest request)
    {
        var id = EntityId.TryParse(request.Id);

        if (id.IsFailure) return new(typeof(DeleteUser), id.Error!);

        return new(new DeleteUser(gateway, new(id.Value)));
    }
}

internal readonly record struct DeleteUserData(EntityId Id);

public class DeleteUser : IDomainCommand
{
    private readonly IUserGateway gateway;
    private readonly DeleteUserData data;

    internal DeleteUser(IUserGateway repository, DeleteUserData data)
    {
        this.gateway = repository;
        this.data = data;
    }

    public async Task Execute()
    {
        await gateway.DeleteUserAsync(data.Id.ToString());

        // Emit message
    }
}