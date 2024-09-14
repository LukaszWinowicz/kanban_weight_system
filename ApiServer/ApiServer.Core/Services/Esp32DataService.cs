using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using MQTTnet;
using MQTTnet.Client;
using System.Text;
using System.Threading;

namespace ApiServer.Core.Services
{
    public class Esp32DataService
    {
        private readonly IScaleRepository _scaleRepository;
        private readonly IMqttClient _mqttClient; // IMqttClient: Reprezentuje klienta MQTT, który będzie używany do nawiązywania połączeń z brokerem MQTT.
        private readonly MqttClientOptions _mqttOptions; // MqttClientOptions: Ustawia opcje klienta, takie jak ID klienta, adres serwera, port i dane uwierzytelniające.
        private CancellationTokenSource _cancellationTokenSource; // Token do anulowania zadania
        private bool _isPollingActive = false; // Flaga wskazująca, czy odpytywanie jest aktywne

        public Esp32DataService(IScaleRepository scaleRepository) 
        {
            _scaleRepository = scaleRepository;

            // Tworzenie fabryki MQTT
            // MqttFactory: Tworzy instancję fabryki klienta MQTT, która umożliwia tworzenie nowych klientów MQTT.
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            // Konfiguracja opcji klienta MQTT
            _mqttOptions = new MqttClientOptionsBuilder()
                .WithClientId("esp32-monitor-server") // Unikalny identyfikator klienta (serwera)
                .WithTcpServer("192.168.1.32", 1883) // Adres i port Twojego brokera MQTT
                .WithCredentials("mqtt_user", "mqtt_pass") // Dane logowania do brokera MQTT
                .Build();
        }

        public IEnumerable<ScaleEntity> ScalesList()
        {
            var entities = _scaleRepository.GetAll();
            return entities;
        }

        public bool IsScaleConnected(string deviceNumber)
        {
            try
            {
                // Połączenie z brokerem MQTT
                _mqttClient.ConnectAsync(_mqttOptions).Wait(); // Synchronicznie oczekujemy na połączenie

                // TaskCompletionSource do oczekiwania na odpowiedź
                var tcs = new TaskCompletionSource<bool>();

                // Obsługa przychodzących wiadomości MQTT
                _mqttClient.ApplicationMessageReceivedAsync += e =>
                {
                    var topic = e.ApplicationMessage.Topic;
                    var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                    // Sprawdź, czy temat wiadomości jest odpowiedzią od ESP32
                    if (topic == $"response/{deviceNumber}")
                    {
                        tcs.SetResult(true); // Ustaw wynik na true, jeśli otrzymamy odpowiedź
                    }

                    return Task.CompletedTask; // Zwraca ukończone zadanie
                };

                // Subskrypcja na temat odpowiedzi od konkretnego ESP32
                _mqttClient.SubscribeAsync($"response/{deviceNumber}").Wait();

                // Wysłanie zapytania do konkretnego ESP32
                var message = new MqttApplicationMessageBuilder()
                    .WithTopic($"request/{deviceNumber}") // Wysyłanie do konkretnego urządzenia
                    .WithPayload("check") // Przykładowa wiadomość "check"
                    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce) // Ustawienie poziomu QoS
                    .WithRetainFlag(false)
                    .Build();

                _mqttClient.PublishAsync(message).Wait(); // Synchronicznie wysyłamy wiadomość

                // Oczekiwanie na odpowiedź przez maksymalnie 5 sekund
                bool isEsp32Connected = tcs.Task.Wait(5000) && tcs.Task.Result;

                // Rozłączenie się z brokerem MQTT
                _mqttClient.DisconnectAsync().Wait(); // Synchronicznie rozłączamy się

                return isEsp32Connected;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas sprawdzania ESP32: {ex.Message}");
                return false;
            }
        }

        //public void StartPollingScales()
        //{
        //    // Sprawdź, czy odpytywanie już jest aktywne, aby zapobiec wielokrotnemu uruchamianiu
        //    if (_isPollingActive)
        //    {
        //        Console.WriteLine("Odpytywanie jest już aktywne.");
        //        return;
        //    }

        //    _isPollingActive = true; // Ustaw flagę na aktywne

        //    // Inicjujemy CancellationTokenSource, który pozwala zatrzymać zadanie w razie potrzeby
        //    _cancellationTokenSource = new CancellationTokenSource();
        //    var cancellationToken = _cancellationTokenSource.Token;

        //    // Uruchamiamy zadanie asynchroniczne, które będzie działać w tle
        //    Task.Run(async () =>
        //    {
        //        while (!cancellationToken.IsCancellationRequested)
        //        {
        //            try
        //            {
        //                // Pobieranie listy urządzeń do odpytywania
        //                var scales = ScalesList();

        //                foreach (var scale in scales)
        //                {
        //                    // Sprawdzenie, czy urządzenie jest połączone
        //                    if (IsScaleConnected(scale.ScaleName))
        //                    {
        //                        // Jeśli urządzenie jest połączone, odczytujemy wartość
        //                        Console.WriteLine($"Urządzenie {scale.ScaleName} jest połączone.");
        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine($"Urządzenie {scale.ScaleName} nie odpowiada.");
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine($"Błąd podczas odpytywania: {ex.Message}");
        //            }

        //            // Oczekiwanie przez minutę przed kolejnym cyklem
        //            await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
        //        }
        //    }, cancellationToken);
        //}

        public void StartPollingScales()
        {
            // Sprawdź, czy odpytywanie jest już aktywne, aby zapobiec wielokrotnemu uruchamianiu
            if (_isPollingActive)
            {
                Console.WriteLine("Odpytywanie jest już aktywne.");
                return;
            }

            _isPollingActive = true; // Ustaw flagę na aktywne

            // Inicjujemy CancellationTokenSource, który pozwala zatrzymać zadanie w razie potrzeby
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;

            // Uruchamiamy zadanie asynchroniczne, które będzie działać w tle
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        // Pobieranie listy urządzeń do odpytywania
                        var scales = ScalesList();

                        foreach (var scale in scales)
                        {
                            // Odczytaj wartość z ESP32 dla każdego urządzenia
                            string response = GetEsp32Value(scale.ScaleName);

                            // Jeśli otrzymaliśmy odpowiedź, wyświetlamy ją w terminalu
                            if (!string.IsNullOrEmpty(response))
                            {
                                Console.WriteLine($"Otrzymana wartość z {scale.ScaleName}: {response}");
                            }
                            else
                            {
                                Console.WriteLine($"Nie otrzymano odpowiedzi od {scale.ScaleName}.");
                            }

                            // Odczekaj sekundę między odpytywaniem kolejnych urządzeń, aby zredukować obciążenie sieci
                            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Błąd podczas odpytywania: {ex.Message}");
                    }

                    // Oczekiwanie przez minutę przed kolejnym cyklem
                    await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
                }
            }, cancellationToken);
        }

        public void StopPollingScales()
        {
            if (!_isPollingActive)
            {
                Console.WriteLine("Odpytywanie nie jest aktywne.");
                return;
            }

            // Anulujemy zadanie
            _cancellationTokenSource.Cancel();
            _isPollingActive = false; // Ustaw flagę na nieaktywne
            Console.WriteLine("Odpytywanie zostało zatrzymane.");
        }

        public string GetEsp32Value(string deviceNumber)
        {
            try
            {
                // Połączenie z brokerem MQTT
                _mqttClient.ConnectAsync(_mqttOptions).Wait();

                // TaskCompletionSource do oczekiwania na odpowiedź
                var tcs = new TaskCompletionSource<string>();

                // Handler obsługujący wiadomości
                Func<MqttApplicationMessageReceivedEventArgs, Task> messageHandler = e =>
                {
                    var topic = e.ApplicationMessage.Topic;
                    var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                    // Sprawdź, czy temat wiadomości jest odpowiedzią od ESP32
                    if (topic == $"response/{deviceNumber}")
                    {
                        if (!tcs.Task.IsCompleted) // Sprawdź, czy zadanie nie jest już zakończone
                        {
                            tcs.SetResult(payload); // Ustaw wynik na payload wiadomości
                        }
                    }

                    return Task.CompletedTask;
                };

                // Subskrybuj wiadomości
                _mqttClient.ApplicationMessageReceivedAsync += messageHandler;

                // Subskrypcja na temat odpowiedzi od konkretnego ESP32
                _mqttClient.SubscribeAsync($"response/{deviceNumber}").Wait();

                // Wysłanie zapytania do konkretnego ESP32
                var message = new MqttApplicationMessageBuilder()
                    .WithTopic($"request/{deviceNumber}") // Wysyłanie do konkretnego urządzenia
                    .WithPayload("check") // Przykładowa wiadomość "check"
                    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce) // QoS: co najwyżej raz
                    .WithRetainFlag(false)
                    .Build();

                _mqttClient.PublishAsync(message).Wait();

                // Oczekiwanie na odpowiedź przez maksymalnie 5 sekund
                bool messageReceived = tcs.Task.Wait(5000);

                // Odsubskrybowanie wiadomości po otrzymaniu odpowiedzi lub po upłynięciu czasu
                _mqttClient.ApplicationMessageReceivedAsync -= messageHandler;

                // Rozłączenie się z brokerem MQTT
                _mqttClient.DisconnectAsync().Wait();

                if (messageReceived)
                {
                    return tcs.Task.Result;
                }
                else
                {
                    return null; // Brak odpowiedzi w ciągu 5 sekund
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas komunikacji z ESP32: {ex.Message}");
                return null; // Zwraca null w przypadku błędu
            }
        }


    }
}
