namespace SeasonalBite.Interfaces;

public interface IFirebaseAuthService
{
    public string Username { get; }

    public bool IsSignedIn { get; }

    public Task SignInAsync(string email, string password);

    public Task CreateUserAsync(string email, string password, string username);

    // TODO IMPLEMENT SIGN OUT
}