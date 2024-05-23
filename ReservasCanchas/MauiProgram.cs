using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Mopups.Hosting;
using ReservasCanchas.LocalDb;
using ReservasCanchas.Services;
using ReservasCanchas.ViewModels;
using ReservasCanchas.Views;
using UraniumUI;

namespace ReservasCanchas
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                    .ConfigureMopups()

                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFontAwesomeIconFonts();

                });
            builder.Services.AddSingleton<CanchasService>();
            builder.Services.AddSingleton<AuthService>();

            builder.Services.AddSingleton<CanchasViewModel>();
            builder.Services.AddSingleton(Connectivity.Current);
            builder.Services.AddTransient<ReservasService>();
            builder.Services.AddCommunityToolkitDialogs();
            builder.Services.AddMopupsDialogs();
            builder.Services.AddSingleton<LocalDbService>();
            builder.Services.AddTransient<CanchaDetailsViewModel>();

            builder.Services.AddTransient<CanchasView>();
            builder.Services.AddSingleton<Login>();

            builder.Services.AddTransient<ReservaViewModel>();

            builder.Services.AddTransient<DetallesCancha>();
            builder.Services.AddTransient<AddReservaView>();
            builder.Services.AddTransient<ReservasView>();

            builder.Services.AddTransient<ReservasCancha>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
