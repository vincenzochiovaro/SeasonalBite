using Firebase.Auth;
using Firebase.Auth.Providers;
using SeasonalBite.Services;
using SeasonalBite.ViewModels;

namespace SeasonalBiteUnitTests.IntegrationTests
{
    public class SignUpViewModelTests
    {
        private const string Email = "integrationTest@test.com";
        private const string Password = "Password123!";
        private const string Username = "integrationTest";
        private readonly FirebaseAuthService _firebaseAuthService;

        public SignUpViewModelTests()
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
        public async Task When_UsernameIsNull_Then_ShouldShowInvalidUsernameError()
        {
            var sut = new SignUpViewModel(_firebaseAuthService);
            sut.Email = Email;
            sut.Password = Password;
            sut.Username = null;

            await sut.SignUpCommand.ExecuteAsync(null);

            Assert.Equal("Username must be at least 3 characters long.", sut.LastErrorMessage);
        }

        [Fact]
        public async Task When_UsernameIsTooShort_Then_ShouldShowInvalidUsernameError()
        {
            var sut = new SignUpViewModel(_firebaseAuthService);
            sut.Email = Email;
            sut.Password = Password;
            sut.Username = "ab";

            await sut.SignUpCommand.ExecuteAsync(null);

            Assert.Equal("Username must be at least 3 characters long.", sut.LastErrorMessage);
        }

        [Fact]
        public async Task When_Missing_Fields_Then_ShouldShowMissingFieldError()
        {
            var sut = new SignUpViewModel(_firebaseAuthService);
            sut.Email = null;
            sut.Password = null;
            sut.Username = Username;

            await sut.SignUpCommand.ExecuteAsync(null);

            Assert.Equal("Please fill in all required fields", sut.LastErrorMessage);
        }
    }
}