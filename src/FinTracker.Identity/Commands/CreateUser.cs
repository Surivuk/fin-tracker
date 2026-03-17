using FinTracker.Identity.Gateway;
using FinTracker.IDomain;

namespace FinTracker.Identity.Commands;

public readonly record struct CreateUserRequest(string Email, string FirstName, string LastName, string Password);

public class CreateUserBuilder(IUserGateway gateway) : IDomainCommandBuilder<CreateUserRequest>
{
    public DomainCommandResult TryWith(CreateUserRequest request)
    {
        var name = Name.New(request.FirstName, request.LastName);
        var email = Email.New(request.Email);
        var credentials = Credentials.New(request.Password);

        if (name.IsFailure) return new(typeof(CreateUser), name.Error!);
        if (email.IsFailure) return new(typeof(CreateUser), email.Error!);
        if (credentials.IsFailure) return new(typeof(CreateUser), credentials.Error!);

        return new(new CreateUser(gateway, new(email.Value, name.Value, credentials.Value)));
    }
}

internal readonly record struct CreateUserData(Email Email, Name Name, Credentials Credentials);

public class CreateUser : IDomainCommand
{
    private readonly IUserGateway gateway;
    private readonly CreateUserData data;

    internal CreateUser(IUserGateway repository, CreateUserData data)
    {
        this.gateway = repository;
        this.data = data;
    }

    public async Task Execute()
    {
        var userId = await gateway.CreateUserAsync(data.Email.ToString(), data.Name.FirstName, data.Name.LastName, data.Credentials.ToString());

        // Emit message
    }
}