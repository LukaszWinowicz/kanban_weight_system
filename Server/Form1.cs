namespace Server
{
    public partial class Form1 : Form
    {
        private readonly MosquittoService _mosquittoService;
        private readonly Esp32DataService _esp32DataService;
        private readonly CmdExecutorService _cmdExecutor;

        public Form1()
        {
            InitializeComponent();
            _mosquittoService = new MosquittoService();
            _esp32DataService = new Esp32DataService("192.168.1.32", "mqtt_user", "mqtt_pass");
            _cmdExecutor = new CmdExecutorService();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _mosquittoService.StartMosquitto();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            base.OnClosed(e);
            _mosquittoService.StopMosquitto();
        }

        private async void subBtn_Click(object sender, EventArgs e)
        {
            await _esp32DataService.StartAsync();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _esp32DataService.Dispose();
            _mosquittoService.Dispose();
        }

        private void cmdBtn_Click(object sender, EventArgs e)
        {
            _cmdExecutor.ExecuteCommand("netstat -an | find \"1883\"");
        }
    }
}
