using SeasonalBite.Interfaces;

namespace SeasonalBite;

public partial class MainPage : ContentPage
{
    private readonly IAlimentHelper _alimentHelper;

    public MainPage(IAlimentHelper alimentHelper)
    {
        InitializeComponent();
        MyLabel.Text = $"Coltivazioni di {DateTime.Now:MMMM}";
        _alimentHelper = alimentHelper;
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