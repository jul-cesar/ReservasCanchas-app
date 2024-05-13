using Microsoft.Extensions.Logging;
using ReservasCanchas.Services;
using ReservasCanchas.ViewModels;
using ReservasCanchas.Views;

namespace ReservasCanchas
{
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
            builder.Services.AddSingleton<CanchasService>();
            builder.Services.AddSingleton<CanchasViewModel>();
            builder.Services.AddSingleton<CanchasView>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
