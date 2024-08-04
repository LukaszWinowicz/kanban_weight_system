using Microsoft.Extensions.Logging;
using MauiDashboardApp.Services;
using MauiDashboardApp.Models;

namespace MauiDashboardApp
{
    public static class MauiProgram
    {
        public static Microsoft.Maui.Hosting.MauiApp CreateMauiApp()
        {
            var builder = Microsoft.Maui.Hosting.MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddTransient<SensorReadingsViewModel>();
            builder.Services.AddTransient<SensorReadingsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}