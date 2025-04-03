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
}