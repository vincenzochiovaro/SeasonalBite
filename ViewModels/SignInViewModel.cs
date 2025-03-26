using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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


    [RelayCommand]
    private async Task SignIn()
    {
        try
        {
            await _firebaseAuthService.SignInAsync(email: _email, password: _password);

            await Task.Delay(1);

            await Shell.Current.GoToAsync("//MainPage");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    [RelayCommand]
    private async Task NavigateSignUp()
    {
        await Shell.Current.GoToAsync("//SignUp");
    }
}