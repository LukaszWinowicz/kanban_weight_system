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
                    .Take(10)  // Pobieramy tylko 10 najnowszych odczyt�w dla przyk�adu
                    .ToListAsync();

                // Wypisujemy dane do debugowania
                foreach (var reading in readings)
                {
                    Debug.WriteLine($"Reading: ID={reading.SensorId}, Date={reading.Date}, EspName={reading.EspName}, EspId={reading.EspId}, Value={reading.Value}");
                }

                await DisplayAlert("Sukces", $"Pobrano {readings.Count} odczyt�w. Sprawd� konsol� debugowania.", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"B��d podczas pobierania danych: {ex.Message}");
                await DisplayAlert("B��d", $"Nie uda�o si� pobra� danych: {ex.Message}", "OK");
            }
        }
}
