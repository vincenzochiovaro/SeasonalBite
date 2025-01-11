using SeasonalBite.Interfaces;

namespace SeasonalBite;

public partial class MainPage : ContentPage
{
    int count = 0;
    private readonly IAlimentRepository _alimentRepository;

    public MainPage(IAlimentRepository alimentRepository)
    {
        InitializeComponent();
        _alimentRepository = alimentRepository;
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        await _alimentRepository.GetAlimentsAsync(); // Todo
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}