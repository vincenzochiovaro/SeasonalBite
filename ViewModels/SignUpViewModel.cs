using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

    [ObservableProperty] private string _username;

    [ObservableProperty] private string _password;

    [RelayCommand]
    private async Task SignUp()
    {
        try
        {
            Console.WriteLine("inside SignUp method");
            await _firebaseAuthService.CreateUserAsync(email: _email, password: _password, username: _username);

            await Task.Delay(3);

            await Shell.Current.GoToAsync("//MainPage");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    [RelayCommand]
    private async Task NavigateSignIn()
    {
        await Shell.Current.GoToAsync("//SignIn");
    }
}