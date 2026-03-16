using FinTracker.Identity.Gateway;
using FinTracker.IDomain;

namespace FinTracker.Identity.Commands;

public readonly record struct ChangeUserCredentialsRequest(string Id, string Password);

public class ChangeUserCredentialsBuilder(IUserGateway gateway) : IDomainCommandBuilder<ChangeUserCredentialsRequest>
{
    public DomainCommandResult TryWith(ChangeUserCredentialsRequest request)
    {
        var id = EntityId.TryParse(request.Id);
        var credentials = Credentials.New(request.Password);

        if (id.IsFailure) return new(typeof(ChangeUserCredentials), id.Error!);
        if (credentials.IsFailure) return new(typeof(ChangeUserCredentials), credentials.Error!);

        return new(new ChangeUserCredentials(gateway, new(id.Value, credentials.Value)));
    }
}

internal readonly record struct ChangeUserCredentialsData(EntityId Id, Credentials Credentials);

public class ChangeUserCredentials : IDomainCommand
{
    private readonly IUserGateway gateway;
    private readonly ChangeUserCredentialsData data;

    internal ChangeUserCredentials(IUserGateway repository, ChangeUserCredentialsData data)
    {
        this.gateway = repository;
        this.data = data;
    }

    public Task Execute() => gateway.ChangeCredentialsAsync(data.Id.ToString(), data.Credentials.ToString());
}