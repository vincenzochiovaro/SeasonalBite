using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using SeasonalBite.Interfaces;

namespace SeasonalBite.ViewModels;

public partial class SignUpViewModel : ObservableObject
{
    private readonly IFirebaseAuthService _firebaseAuthService;

    public SignUpViewModel(IFirebaseAuthService firebaseAuthService)
    {
        _firebaseAuthService = firebaseAuthService;
    }

    [ObservableProperty] private string _email;

    [ObservableProperty] private string? _username;

    [ObservableProperty] private string _password;

    [ObservableProperty] private string _errorMessage;

    [ObservableProperty] private string _lastErrorMessage;

    [RelayCommand]
    private async Task SignUp()
    {
        try
        {
            if (Username == null) throw new FirebaseAuthException("InvalidUsername", new AuthErrorReason());
            if (Username != null && Username.Length <= 2)
                throw new FirebaseAuthException("InvalidUsername", new AuthErrorReason());

            await _firebaseAuthService.CreateUserAsync(email: _email, password: _password, username: _username);
            
            Email = string.Empty;
            Password = string.Empty;
            Username = string.Empty;
            
            await Task.Delay(3);

            await Shell.Current.GoToAsync("//MainPage");
        }
        catch (FirebaseAuthException ex)
        {
            if (ex.Message == "InvalidUsername")
            {
                ErrorMessage = "Username must be at least 3 characters long.";
                LastErrorMessage = "Username must be at least 3 characters long.";

                await Task.Delay(3000);
                ErrorMessage = "";
                return;
            }
            
            if (ex.Reason.ToString() == "Unknown")
            {
                ErrorMessage = "Please fill in all required fields";
                LastErrorMessage = "Please fill in all required fields";

                await Task.Delay(3000);
                ErrorMessage = "";
                return;
            }

            LastErrorMessage = ex.Reason.ToString();
            ErrorMessage = ex.Reason.ToString();
            await Task.Delay(3000);
            ErrorMessage = "";
        }
    }

    [RelayCommand]
    private async Task NavigateSignIn()
    {
        await Shell.Current.GoToAsync("//SignIn");
    }
    
    [RelayCommand]
    private async Task Guest()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}