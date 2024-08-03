using ApiServer.Database;
using ApiServer.Models;
using ApiServer.Services;
using Microsoft.EntityFrameworkCore;

namespace ApiServer
{
    public partial class Form1 : Form
    {
        private readonly AppDbContext _context;
        private List<SensorReading> _loadedData; // Przechowuje za³adowane dane
        private readonly MosquittoService _mosquittoService;
        private readonly Esp32DataService _esp32DataService;
        private readonly CmdExecutorService _cmdExecutor;

        public Form1(AppDbContext context)
        {
            InitializeComponent();
            _mosquittoService = new MosquittoService();
            _esp32DataService = new Esp32DataService("192.168.1.32", "mqtt_user", "mqtt_pass");
            _cmdExecutor = new CmdExecutorService();
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

        private void btnRunMqtt_Click(object sender, EventArgs e)
        {
            _mosquittoService.StartMosquitto();
        }

        private void btnCloseMqtt_Click(object sender, EventArgs e)
        {
            base.OnClosed(e);
            _mosquittoService.StopMosquitto();
        }

        private async void subBtn_Click(object sender, EventArgs e)
        {
            await _esp32DataService.StartAsync();
        }

        private void btnCmd_Click(object sender, EventArgs e)
        {
            _cmdExecutor.ExecuteCommand("netstat -an | find \"1883\"");
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _esp32DataService.Dispose();
            _mosquittoService.Dispose();
        }
    }
}
