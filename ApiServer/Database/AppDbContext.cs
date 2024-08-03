using ApiServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiServer.Database
{
    public class AppDbContext : DbContext
    {    
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<SensorReading> SensorReadings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           /* modelBuilder.Entity<SensorReading>()
                .HasKey(s => s.SensorId);

            modelBuilder.Entity<SensorReading>()
                .Property(s => s.EspName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<SensorReading>()
                .Property(s => s.EspId)
                .IsRequired()
                .HasMaxLength(50);*/
        }
    }
}
