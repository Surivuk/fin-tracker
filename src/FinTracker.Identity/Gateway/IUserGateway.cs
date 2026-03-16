namespace FinTracker.Identity.Gateway;

public interface IUserGateway
{
    public Task<string> CreateUserAsync(string email, string firstName, string lastName, string password);
    public Task ChangeNameAsync(string id, string firstName, string lastName);
    public Task ChangeCredentialsAsync(string id, string password);
    public Task DeleteUserAsync(string id);
}