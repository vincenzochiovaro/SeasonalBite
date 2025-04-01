using Firebase.Auth;
using Firebase.Auth.Providers;
using SeasonalBite.Services;
using SeasonalBite.ViewModels;

namespace SeasonalBiteUnitTests.IntegrationTests;

public class SignInViewModelTests
{
    private const string Email = "integrationTest@test.com";
    private const string Password = "Password123!";
    private const string Username = "integrationTest";
    private readonly FirebaseAuthService _firebaseAuthService;
    
    public SignInViewModelTests()
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
        _firebaseAuthService = new FirebaseAuthService(firebaseAuthClient);
    }
    
    [Fact]
    public async Task When_InvalidCredentials_Then_ShouldShowInvalidCredentialsError()
    {
        var sut = new SignInViewModel(_firebaseAuthService);
        sut.Email = Email;
        sut.Password = Guid.NewGuid().ToString();

        await sut.SignInCommand.ExecuteAsync(null);

        Assert.Equal("Invalid Credentials", sut.LastErrorMessage);
    }
}