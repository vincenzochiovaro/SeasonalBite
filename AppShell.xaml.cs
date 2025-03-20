using SeasonalBite.Views;

namespace SeasonalBite;

public partial class AppShell : Shell
{
    public string ShellTitle { get; set; }

    public AppShell()
    {
        InitializeComponent();
        ShellTitle = $"SeasonalBite - {DateTime.Now:MMMM}";
        BindingContext = this;
        
        Routing.RegisterRoute("SignIn", typeof(SignInView));
        Routing.RegisterRoute("SignUp", typeof(SignUpView));
        Routing.RegisterRoute("MainPage", typeof(MainPage));
    }
}