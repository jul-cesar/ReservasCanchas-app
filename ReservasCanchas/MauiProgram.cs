using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFontAwesomeIconFonts();

                });
            builder.Services.AddSingleton<CanchasService>();
            builder.Services.AddSingleton<CanchasViewModel>();
            builder.Services.AddSingleton(Connectivity.Current);
            builder.Services.AddTransient<CanchaDetailsViewModel>();

            builder.Services.AddSingleton<CanchasView>();
            builder.Services.AddTransient<ReservaViewModel>();

            builder.Services.AddTransient<DetallesCancha>();
            builder.Services.AddSingleton<AddReservaView>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
