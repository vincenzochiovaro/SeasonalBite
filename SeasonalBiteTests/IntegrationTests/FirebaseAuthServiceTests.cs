using Firebase.Auth;
using Firebase.Auth.Providers;
using SeasonalBite.Services;

namespace SeasonalBiteUnitTests.IntegrationTests;

public class FirebaseAuthServiceTests
{
    private readonly FirebaseAuthService _sut;

    private const string Email = "integrationTest@test.com";
    private const string Password = "Password123!";
    private const string Username = "integrationTest";

    public FirebaseAuthServiceTests()
    {
        var authConfig = new FirebaseAuthConfig
        {
            ApiKey = "AIzaSyDowsHYKgrlRuTrK4CDORHsj5iPAzhePLo",
            AuthDomain = "seasonal-bite.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]
            {
                new EmailProvider()
            },
        };

        var firebaseAuthClient = new FirebaseAuthClient(authConfig);

        _sut = new FirebaseAuthService(firebaseAuthClient);
    }

    [Fact]
    public async Task GivenValidCredentials_WhenUserSignIn_ThenUsernameIsSetCorrectly()
    {
        if (await IsUserSignedIn(Email) == false)
        {
            await _sut.CreateUserAsync(Email, Password, Username);

            await Task.Delay(5);

            await _sut.SignInAsync(Email, Password);
        }

        Assert.Equal(Username, _sut.Username);
    }

    [Fact]
    public async Task GivenInValidCredentials_WhenUserSignIn_ThenUsernameIsNull()
    {
        await IsUserSignedIn("invalidEmail@invalid.com");

        Assert.Null(_sut.Username);
    }

    [Fact]
    public async Task GivenAlreadyExistingEmail_WhenCreateUser_ThenThrowsException()
    {
        try
        {
            await _sut.CreateUserAsync(Email, Password, Username);

            await _sut.CreateUserAsync(Email, Password, Username);
        }
        catch(FirebaseAuthException ex)
        {
            Assert.Equal("EmailExists", ex.Reason.ToString());
            await Assert.ThrowsAsync<FirebaseAuthHttpException>(() => _sut.CreateUserAsync(Email, Password, Username));
        }
    }

    [Fact]
    public async Task GivenWeakPassowrd_WhenCreateUser_ThenThrowsException()
    {
        try
        {
            await _sut.CreateUserAsync("testemail@test.com", "weak", Username);
        }
        catch (FirebaseAuthException ex)
        {
            Assert.Equal("WeakPassword", ex.Reason.ToString());
            await Assert.ThrowsAsync<FirebaseAuthHttpException>(() =>
                _sut.CreateUserAsync("testemail@test.com", "weak", Username));
        }
    }

    [Fact]
    public async Task SIGNOUT_PLACEHOLDER_TODO()
    {
    }

    private async Task<bool> IsUserSignedIn(string userEmail)
    {
        try
        {
            await _sut.SignInAsync(userEmail, Password);

            return _sut.Username == Username;
        }
        catch
        {
            return false;
        }
    }
}