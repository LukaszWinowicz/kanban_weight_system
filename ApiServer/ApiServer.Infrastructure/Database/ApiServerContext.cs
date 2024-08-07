using ApiServer.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiServer.Infrastructure.Database
{
    public class ApiServerContext : DbContext
    {
        public DbSet<SensorReadingEntity> SensorReadings { get; set; }
        public DbSet<ScaleConfiguration> ScaleConfigurations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=localhost;database=ApiServerDB;trusted_connection=true;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ScaleConfiguration>(entity =>
            {
                entity.Property(e => e.SingleItemWeight).HasColumnType("decimal(18,4)");
            });
        }
    }
}
