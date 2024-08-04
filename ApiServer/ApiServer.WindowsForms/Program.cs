using ApiServer.Infrastructure.Database;
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

            var builder = WebApplication.CreateBuilder();
            ConfigureServices(builder.Services);

            var app = builder.Build();
            ConfigureWebApi(app);

            // Uruchom Web API w osobnym w¹tku
            var apiThread = new Thread(() =>
            {
                app.Run("http://localhost:5000");
            });
            apiThread.Start();

            var services = new ServiceCollection();
            ConfigureServices(services);

            // Uruchom aplikacjê WinForms
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var form1 = serviceProvider.GetRequiredService<Form1>();
                Application.Run(form1);
            }

            // Zatrzymaj API po zamkniêciu formularza
            app.StopAsync().Wait();
            apiThread.Join();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiServerContext>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<Form1>();
        }

        private static void ConfigureWebApi(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}