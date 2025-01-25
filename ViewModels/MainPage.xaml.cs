using SeasonalBite.Interfaces;

namespace SeasonalBite;

public partial class MainPage : ContentPage
{
    int count = 0;
    private readonly IAlimentHelper _alimentHelper;

    public MainPage(IAlimentHelper alimentHelper)
    {
        InitializeComponent();
        _alimentHelper = alimentHelper;
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        // todo
        await _alimentHelper.FilterAlimentsInSeason(1); 
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}