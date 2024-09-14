using ApiServer.Core.Interfaces;
using ApiServer.Core.Services;
using ApiServer.Infrastructure.Repositories;
using ApiServer.Infrastructure.Database;

namespace ApiServer.WindowsForms
{
    public partial class Form1 : Form
    {
        // netstat -an | find "1883"

        private readonly MosquittoService _mosquittoService;
        private readonly Esp32DataService _esp32DataService;

        public Form1()
        {
            InitializeComponent();
            _mosquittoService = new MosquittoService();

            // Tworzenie instancji ApiServerContext
            ApiServerContext context = new ApiServerContext();

            // Tworzenie instancji ScaleRepository z przekazanym kontekstem
            IScaleRepository scaleRepository = new ScaleRepository(context);

            // Tworzenie instancji serwisu Esp32DataService z repozytorium
            _esp32DataService = new Esp32DataService(scaleRepository);
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_esp32DataService != null)
            {
                _esp32DataService.StartPollingScales();
            }
            else
            {
                Console.WriteLine("Obiekt _esp32DataService nie zosta³ zainicjalizowany.");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_esp32DataService != null)
            {
                _esp32DataService.StopPollingScales();
            }
            else
            {
                Console.WriteLine("Obiekt _esp32DataService nie zosta³ zainicjalizowany.");
            }
        }
    }
}
