using System.Globalization;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using SeasonalBite.Data;
using SeasonalBite.Helpers;
using SeasonalBite.Interfaces;
using SeasonalBite.Services;
using SeasonalBite.ViewModels;
using SeasonalBite.Views;

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


#if DEBUG
        builder.Logging.AddDebug();
#endif

        CultureInfo.CurrentCulture = new CultureInfo("it-IT");
        CultureInfo.CurrentUICulture = new CultureInfo("it-IT");

        builder.Services.AddSingleton<IDbManager, CockroachDb>();
        builder.Services.TryAddScoped<IAlimentRepository, CockroachDbRepository>();
        builder.Services.TryAddScoped<IAlimentHelper, AlimentsHelper>();

        builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
        {
            ApiKey = "AIzaSyDowsHYKgrlRuTrK4CDORHsj5iPAzhePLo",
            AuthDomain = "seasonal-bite.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]
            {
                new EmailProvider()
            },
            UserRepository = new FileUserRepository("seasonal-bite.firebaseapp.com")
        }));

        builder.Services.AddSingleton<IFirebaseAuthService, FirebaseAuthService>();

        builder.Services.AddSingleton<SignInView>();
        builder.Services.AddSingleton<SignUpView>();

        builder.Services.AddSingleton<SignInViewModel>();
        builder.Services.AddSingleton<SignUpViewModel>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<AppShell>();

        return builder.Build();
    }
}