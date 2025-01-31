namespace SeasonalBite;

public partial class AppShell : Shell
{
    public string ShellTitle { get; set; }

    public AppShell()
    {
        InitializeComponent();
        ShellTitle = $"SeasonalBite - {DateTime.Now:MMMM}";
        BindingContext = this;
    }
}