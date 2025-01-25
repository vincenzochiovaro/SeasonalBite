using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using SeasonalBite.Data;
using SeasonalBite.Helpers;
using SeasonalBite.Interfaces;
using SeasonalBite.Services;

namespace SeasonalBite;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<IDbManager, CockroachDb>();
        builder.Services.TryAddScoped<IAlimentRepository, CockroachDbRepository>();
        builder.Services.TryAddScoped<IAlimentHelper, AlimentsHelper>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<AppShell>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}