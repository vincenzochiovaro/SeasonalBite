using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;

namespace SeasonalBite.ViewModels;

public partial class SignInViewModel : ObservableObject
{
    private readonly FirebaseAuthClient _firebaseAuth;
    
    public SignInViewModel(FirebaseAuthClient firebaseAuthClient)
    {
        _firebaseAuth = firebaseAuthClient;
    }
    
    [ObservableProperty]
    private string _email;
    
    [ObservableProperty]
    private string _password;
    
    public string Username => _firebaseAuth.User?.Info.DisplayName;

    [RelayCommand]
    private async Task SignIn()
    {
        try
        {
           await  _firebaseAuth.SignInWithEmailAndPasswordAsync(email: _email, password: _password);
           
           OnPropertyChanged(nameof(Username));
           
           Console.WriteLine("success");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    [RelayCommand]
    private async Task NavigateToSignUp()
    {
        await Shell.Current.GoToAsync("//SignUp");
    }
}