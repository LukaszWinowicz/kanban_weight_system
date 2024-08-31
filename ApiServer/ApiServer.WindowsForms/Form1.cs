namespace ApiServer.WindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
    }
}
