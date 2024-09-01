using ApiServer.WindowsForms.Services;

namespace ApiServer.WindowsForms
{
    public partial class Form1 : Form
    {
        // netstat -an | find "1883"

        private readonly MosquittoService _mosquittoService;

        public Form1()
        {
            InitializeComponent();
            _mosquittoService = new MosquittoService();
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
        }
    }
}
