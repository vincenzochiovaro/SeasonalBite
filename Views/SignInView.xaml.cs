using SeasonalBite.ViewModels;

namespace SeasonalBite.Views;

public partial class SignInView : ContentPage
{
    public SignInView(SignInViewModel signInViewModel)
    {
        InitializeComponent();
        
        BindingContext = signInViewModel;
    }
}