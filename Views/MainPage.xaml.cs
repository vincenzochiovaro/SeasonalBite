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
        // IS THE user signed in? if yes go to AI page with the aliment otherwise go to sign in
        var aliment = ((Button)sender).CommandParameter as Aliment;

        await Shell.Current.GoToAsync("//SignIn");
    }
}