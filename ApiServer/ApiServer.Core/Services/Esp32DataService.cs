using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using MQTTnet;
using MQTTnet.Client;
using System.Text;

namespace ApiServer.Core.Services
{
    public class Esp32DataService
    {
        private readonly IScaleRepository _scaleRepository;
        private readonly IMqttClient _mqttClient; // IMqttClient: Reprezentuje klienta MQTT, który będzie używany do nawiązywania połączeń z brokerem MQTT.
        private readonly MqttClientOptions _mqttOptions; // MqttClientOptions: Ustawia opcje klienta, takie jak ID klienta, adres serwera, port i dane uwierzytelniające.
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

        //public bool IsScaleConnectedAsync(string scaleName)
        //{
        //    var scales = ScalesList();

        //    var scale = scales.FirstOrDefault(s => s.ScaleName.Equals(scaleName, StringComparison.OrdinalIgnoreCase));

        //    return scale != null && scale.IsConnected;

        //}
    }
}
