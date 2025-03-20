using SeasonalBite.ViewModels;

namespace SeasonalBite.Views;

public partial class SignUpView : ContentPage
{
    public SignUpView(SignUpViewModel signUpViewModel)
    {
        InitializeComponent();

        BindingContext = signUpViewModel;
    }
}