using MauiDashboardApp.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MauiDashboardApp
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

            builder.Services.AddDbContext<AppDbContext>(options =>
                            options.UseSqlServer("server=localhost;database=MyDatabase;trusted_connection=true;TrustServerCertificate=True"));
            builder.Services.AddTransient<DatabasePage>();

            #if DEBUG
            builder.Logging.AddDebug();
            #endif

            return builder.Build();
        }
    }
}
