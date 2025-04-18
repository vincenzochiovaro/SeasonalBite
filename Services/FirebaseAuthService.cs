using System.ComponentModel;
using Firebase.Auth;
using SeasonalBite.Interfaces;

namespace SeasonalBite.Services;

public class FirebaseAuthService : IFirebaseAuthService, INotifyPropertyChanged
{
    private readonly FirebaseAuthClient _firebaseAuthClient;
    private string _username;

    public FirebaseAuthService(FirebaseAuthClient firebaseAuthClient)
    {
        _firebaseAuthClient = firebaseAuthClient;
        
        if (_firebaseAuthClient.User != null)
        {
            Username = _firebaseAuthClient.User.Info.DisplayName;
        }
    }

    public string Username
    {
        get => _username;
        private set
        {
            if (_username != value)
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
    }

    public bool IsSignedIn => _firebaseAuthClient.User != null;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public async Task SignInAsync(string email, string password)
    {
        var userCredential = await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(email, password);
        Username = userCredential.User.Info.DisplayName;
    }

    public async Task CreateUserAsync(string email, string password, string username)
    {
        var userCredential = await _firebaseAuthClient.CreateUserWithEmailAndPasswordAsync(email, password, username);
        Username = userCredential.User.Info.DisplayName;
    }
}