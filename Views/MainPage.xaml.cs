using SeasonalBite.Interfaces;
using SeasonalBite.Models;

namespace SeasonalBite;

public partial class MainPage : ContentPage
{
    private readonly IAlimentHelper _alimentHelper;
    private readonly IFirebaseAuthService _firebaseAuthService;

    public MainPage(IAlimentHelper alimentHelper, IFirebaseAuthService firebaseAuthService)
    {
        InitializeComponent();
        _alimentHelper = alimentHelper;
        _firebaseAuthService = firebaseAuthService;
        BindingContext = _firebaseAuthService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await InitializeDataAsync();
    }

    private async Task InitializeDataAsync()
    {
        int currentMonth = DateTime.Now.Month;
        var seasonAliments = await _alimentHelper.FilterAlimentsInSeason(currentMonth);

        ListSeasonAliments.ItemsSource = seasonAliments;
    }

    private async void OnGenerateRecipeClickedAI(object sender, EventArgs e)
    {

        if (_firebaseAuthService.Username == null)
        {
            await Shell.Current.GoToAsync("//SignIn");
            return;
        }

        var aliment = ((Button)sender).CommandParameter as Aliment;

        await Application.Current.MainPage.DisplayAlert(
            "Oops, Not Yet! 🤖",
            $"I see you're curious about {aliment.Name}... but hold on! The AI is still in the kitchen, " +
            $"probably arguing with a virtual chef. 🍳 Try again in a few days—good things take time! 🚀",
            "Got it!"
        );
    }
}