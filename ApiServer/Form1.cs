using ApiServer.Database;
using ApiServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiServer
{
    public partial class Form1 : Form
    {
        private readonly AppDbContext _context;
        private List<SensorReading> _loadedData; // Przechowuje za³adowane dane

        public Form1(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private async void btnLoadDb_Click(object sender, EventArgs e)
        {
            try
            {
                // Pobierz dane z bazy
                _loadedData = await _context.SensorReadings.ToListAsync();
                MessageBox.Show($"Za³adowano {_loadedData.Count} rekordów z bazy danych.");
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Wyst¹pi³ b³¹d podczas ³adowania danych: {ex.Message}", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

    }
}
