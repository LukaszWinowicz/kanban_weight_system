using ApiServer.API.Controllers;
using ApiServer.Core.Interfaces;
using ApiServer.Core.Services;
using ApiServer.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace ApiServer.API
{
    public static class ApiConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddApplicationPart(typeof(HealthController).Assembly)
                    .AddApplicationPart(typeof(ReadingsController).Assembly)
                    .AddApplicationPart(typeof(ScaleController).Assembly)
                    .AddApplicationPart(typeof(ScaleReadingsController).Assembly)
                    .AddControllersAsServices();

            services.AddScoped<IReadingsRepository, ReadingsRepository>();
            services.AddScoped<IReadingsService, ReadingsService>();
            services.AddScoped<IScaleRepository, ScaleRepository>();
            services.AddScoped<IScaleService, ScaleService>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ApiServer API",
                    Version = "v1",
                    Description = "API dla systemu zarządzania skalami i odczytami"
                });

                // Opcjonalnie: Dodaj komentarze XML do Swaggera
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
              //  c.IncludeXmlComments(xmlPath);
            });
        }

        public static void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiServer API V1");
                    c.RoutePrefix = string.Empty; // Aby Swagger UI był dostępny na stronie głównej
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}