using ApiServer.Infrastructure.Database;
using ApiServer.API;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace ApiServer.WindowsForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);

            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                ContentRootPath = AppContext.BaseDirectory,
                ApplicationName = typeof(Program).Assembly.FullName,
                EnvironmentName = Environments.Development // Ustawia �rodowisko na Development
            });

            ConfigureServices(builder.Services);
            ApiConfiguration.ConfigureServices(builder.Services);

            var app = builder.Build();
            ApiConfiguration.Configure(app);

            // Uruchom Web API w osobnym w�tku
            var apiThread = new Thread(() =>
            {
                app.Run("http://localhost:5000");
            });
            apiThread.Start();

            // Uruchom aplikacj� WinForms
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var form1 = serviceProvider.GetRequiredService<Form1>();
                Application.Run(form1);
            }

            // Zatrzymaj API po zamkni�ciu formularza
            app.StopAsync().Wait();
            apiThread.Join();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiServerContext>();
            services.AddScoped<Form1>();
        }

    }
}