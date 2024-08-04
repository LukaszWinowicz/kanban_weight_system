using ApiServer.API;

namespace ApiServer.WindowsForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Uruchom API w osobnym w¹tku
            Task.Run(() => ApiServer.API.Program.StartApi());

            Application.Run(new Form1());
        }
    }
}