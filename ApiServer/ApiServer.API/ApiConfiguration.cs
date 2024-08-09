using ApiServer.API.Controllers;
using ApiServer.Core.Interfaces;
using ApiServer.Core.Services;
using ApiServer.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiServer.API
{
    public static class ApiConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddApplicationPart(typeof(HealthController).Assembly)
                    .AddApplicationPart(typeof(SensorReadingsController).Assembly)
                    .AddControllersAsServices();
            services.AddScoped<IReadingsRepository, ReadingsRepository>();
            services.AddScoped<IReadingsService, ReadingsService>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public static void Configure(WebApplication app)
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
