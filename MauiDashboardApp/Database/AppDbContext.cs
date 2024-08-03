using MauiDashboardApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MauiDashboardApp.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<SensorReading> SensorReadings { get; set; }

        // Metoda pozwala na wskazanie i konfigurację źródła danych
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost;database=MyDatabase;trusted_connection=true;TrustServerCertificate=True");

        }

        //public AppDbContext(DbContextOptions<AppDbContext> options)
        //    : base(options)
        //{
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Możesz dodać dodatkowe konfiguracje modelu tutaj, jeśli są potrzebne
        //}
    }
}
