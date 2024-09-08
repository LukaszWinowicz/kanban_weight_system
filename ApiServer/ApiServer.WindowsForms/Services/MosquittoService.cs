using System.Diagnostics;
using System.Net.Sockets;

namespace ApiServer.WindowsForms.Services
{
    public class MosquittoService : IDisposable
    {
        private Process? _mosquittoProcess;

        public void StartMosquitto()
        {
            // wcześniej mosquitto musi być zainstalowane (zgodnie z instrukcją)
            string batFilePath = @"C:\Program Files\mosquitto\start_mosquitto.bat";

            _mosquittoProcess = new Process();
            _mosquittoProcess.StartInfo.FileName = batFilePath;
            _mosquittoProcess.StartInfo.CreateNoWindow = true;
            _mosquittoProcess.StartInfo.UseShellExecute = false;

            Console.WriteLine("Uruchamianie Mosquitto...");
            _mosquittoProcess.Start();

            // Czekanie na uruchomienie Mosquitto
            Thread.Sleep(3000);
            Console.WriteLine("Mosquitto uruchomione.");
        }

        public void StopMosquitto()
        {
            Console.WriteLine("Zatrzymywanie Mosquitto...");

            // Znajdź wszystkie procesy Mosquitto
            Process[] mosquittoProcesses = Process.GetProcessesByName("mosquitto");

            foreach (Process process in mosquittoProcesses)
            {
                try
                {
                    process.Kill();
                    process.WaitForExit(); // Czekaj, aż proces się zakończy
                    Console.WriteLine("Mosquitto zatrzymane.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Nie udało się zatrzymać Mosquitto: {ex.Message}");
                }
            }

            if (mosquittoProcesses.Length == 0)
            {
                Console.WriteLine("Nie znaleziono działających procesów Mosquitto.");
            }
        }

        public void Dispose()
        {
            StopMosquitto();
        }
    }
}
