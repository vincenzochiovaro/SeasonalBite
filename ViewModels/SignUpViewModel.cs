using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;

namespace SeasonalBite.ViewModels;

public partial class SignUpViewModel : ObservableObject
{
    private readonly FirebaseAuthClient _firebaseAuth;

    public SignUpViewModel(FirebaseAuthClient firebaseAuthClient)
    {
        _firebaseAuth = firebaseAuthClient;
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
            await _firebaseAuth.CreateUserWithEmailAndPasswordAsync(email: _email, password: _password,
                displayName: _username);

            await Task.Delay(5);
            await Shell.Current.GoToAsync("//SignIn");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    [RelayCommand]
    private async Task NavigateToSignIn()
    {
        await Shell.Current.GoToAsync("//SignIn");
    }
}