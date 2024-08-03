using MauiDashboardApp.Database;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MauiDashboardApp;

public partial class DatabasePage : ContentPage
{
        private readonly AppDbContext _context;

        public DatabasePage(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private async void OnFetchDataClicked(object sender, EventArgs e)
        {
            try
            {
                var readings = await _context.SensorReadings
                    .OrderByDescending(r => r.Date)
                    .Take(10)  // Pobieramy tylko 10 najnowszych odczytów dla przyk³adu
                    .ToListAsync();

                // Wypisujemy dane do debugowania
                foreach (var reading in readings)
                {
                    Debug.WriteLine($"Reading: ID={reading.SensorId}, Date={reading.Date}, EspName={reading.EspName}, EspId={reading.EspId}, Value={reading.Value}");
                }

                await DisplayAlert("Sukces", $"Pobrano {readings.Count} odczytów. SprawdŸ konsolê debugowania.", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"B³¹d podczas pobierania danych: {ex.Message}");
                await DisplayAlert("B³¹d", $"Nie uda³o siê pobraæ danych: {ex.Message}", "OK");
            }
        }
}
