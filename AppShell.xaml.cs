using SeasonalBite.Views;

namespace SeasonalBite;

public partial class AppShell : Shell
{
    public string ShellTitle { get; set; }

    public AppShell()
    {
        InitializeComponent();
        var month = $"{DateTime.Now:MMMM}";
        ShellTitle = $"SeasonalBite - " + char.ToUpper(month[0]) + month[1..]; ;
        BindingContext = this;
        
        Routing.RegisterRoute("SignIn", typeof(SignInView));
        Routing.RegisterRoute("SignUp", typeof(SignUpView));
        Routing.RegisterRoute("MainPage", typeof(MainPage));
    }
}