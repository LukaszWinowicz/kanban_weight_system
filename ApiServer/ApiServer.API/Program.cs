using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiServer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services);

            var app = builder.Build();

            ConfigureApp(app);

            app.Run();
        }

        public static void StartApi()
        {
            var builder = WebApplication.CreateBuilder();

            ConfigureServices(builder.Services);

            var app = builder.Build();

            ConfigureApp(app);

            app.RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Dodaj te linie
            services.AddAuthorization();
            services.AddAuthentication();
        }

        private static void ConfigureApp(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Dodaj tê liniê przed UseAuthorization
            app.UseAuthentication();

            app.UseAuthorization();
            app.MapControllers();
        }
    }
}