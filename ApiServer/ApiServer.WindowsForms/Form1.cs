using ApiServer.WindowsForms.Services;

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
            _esp32DataService = new Esp32DataService("192.168.1.32", "mqtt_user", "mqtt_pass");
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

        private async void btnSub_Click(object sender, EventArgs e)
        {
            await _esp32DataService.StartAsync();
        }
    }
}
