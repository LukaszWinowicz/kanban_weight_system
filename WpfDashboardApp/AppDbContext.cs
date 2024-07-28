using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;

namespace WpfDashboardApp
{
    public class AppDbContext : DbContext
    {
        public DbSet<ReadData> ReadDatas { get; set; }

        // Metoda pozwala na wskazanie i konfigurację źródła danych
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost;database=MyDatabase;trusted_connection=true;TrustServerCertificate=True");

        }
    }
}
