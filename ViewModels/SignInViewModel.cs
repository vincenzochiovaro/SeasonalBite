using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using SeasonalBite.Interfaces;

namespace SeasonalBite.ViewModels;

public partial class SignInViewModel : ObservableObject
{
    private readonly IFirebaseAuthService _firebaseAuthService;

    public SignInViewModel(IFirebaseAuthService firebaseAuthService)
    {
        _firebaseAuthService = firebaseAuthService;
    }

    [ObservableProperty] private string _email;

    [ObservableProperty] private string _password;

    [ObservableProperty] private string _errorMessage;

    [ObservableProperty] private string _lastErrorMessage;

    [RelayCommand]
    private async Task SignIn()
    {
        try
        {
            await _firebaseAuthService.SignInAsync(email: _email, password: _password);
            
            Email = string.Empty;
            Password = string.Empty;
            
            await Task.Delay(1);

            await Shell.Current.GoToAsync("//MainPage");
        }
        catch (FirebaseAuthException ex)
        {
            if (ex.Reason.ToString() == "Unknown")
            {
                LastErrorMessage = "Invalid Credentials";
                ErrorMessage = "Invalid Credentials";
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
    private async Task NavigateSignUp()
    {
        await Shell.Current.GoToAsync("//SignUp");
    }
    
    [RelayCommand]
    private async Task Guest()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}