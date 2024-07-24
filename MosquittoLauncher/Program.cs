using System;
using System.Diagnostics;
using System.Threading;

namespace MosquittoLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ścieżka do pliku wsadowego
            string batFilePath = @"C:\Program Files\mosquitto\start_mosquitto.bat";

            // Uruchomienie pliku wsadowego
            Process mosquittoProcess = new Process();
            mosquittoProcess.StartInfo.FileName = batFilePath;
            mosquittoProcess.StartInfo.CreateNoWindow = true;
            mosquittoProcess.StartInfo.UseShellExecute = false;

            Console.WriteLine("Uruchamianie Mosquitto...");
            mosquittoProcess.Start();

            // Czekanie na uruchomienie Mosquitto
            Thread.Sleep(3000);

            // Twój kod aplikacji
            Console.WriteLine("Mosquitto uruchomione. Uruchamianie aplikacji...");

            // Przykładowy kod aplikacji
            // ...

            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć...");
            Console.ReadKey();

            // Zatrzymanie Mosquitto po zakończeniu aplikacji
            StopMosquitto();

            Console.WriteLine("Mosquitto zatrzymane.");
        }

        static void StopMosquitto()
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
    }
}
