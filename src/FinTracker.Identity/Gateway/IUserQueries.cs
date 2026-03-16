namespace FinTracker.Identity.Gateway;

public record UserData(string Email, string FirstName, string LastName);

public interface IUserQueries
{
    public Task<UserData> GetUser();
}