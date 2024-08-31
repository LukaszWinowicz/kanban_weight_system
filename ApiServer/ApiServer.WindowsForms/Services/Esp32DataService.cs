using ApiServer.Core.Interfaces;
using MQTTnet;
using MQTTnet.Client;

namespace ApiServer.WindowsForms.Services
{
    public class Esp32DataService : IDisposable
    {
        private readonly IMqttClient _mqttClient;
        private readonly MqttClientOptions _mqttOptions;
        private readonly string _logFilePath;
        private readonly Dictionary<string, DateTime> _lastPingTime;
        private readonly int _pingInterval = 10; // sekundy
        private readonly IScaleRepository _scaleRepository;
        private List<string> _devices;
        private CancellationTokenSource _cts;

        public Esp32DataService(string mqttServer, string mqttUser, string mqttPass, IScaleRepository scaleRepository)
        {
            var mqttFactory = new MqttFactory();
            _mqttClient = mqttFactory.CreateMqttClient();

            _mqttOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(mqttServer, 1883)
                .WithCredentials(mqttUser, mqttPass)
                .Build();

            _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "esp32_data.txt");
            _lastPingTime = new Dictionary<string, DateTime>();
            _scaleRepository = scaleRepository;
        }

        public async Task StartAsync()
        {
            _cts = new CancellationTokenSource();

            // Pobierz listę urządzeń z repozytorium
            var scales = _scaleRepository.GetAll();
            _devices = scales.Select(s => s.ScaleName).ToList();

            _mqttClient.ConnectedAsync += HandleConnectedAsync;
            _mqttClient.ApplicationMessageReceivedAsync += HandleMessageReceivedAsync;

            await _mqttClient.ConnectAsync(_mqttOptions, _cts.Token);
            Console.WriteLine("Connected to MQTT broker");

            _ = MonitoringLoopAsync(_cts.Token);
        }

        public async Task StopAsync()
        {
            _cts?.Cancel();
            await _mqttClient.DisconnectAsync();
        }

        private Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
        {
            foreach (var device in _devices)
            {
                _mqttClient.SubscribeAsync($"response/{device}");
            }
            return Task.CompletedTask;
        }

        private Task HandleMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            string deviceId = eventArgs.ApplicationMessage.Topic.Split('/')[1];
            if (_devices.Contains(deviceId))
            {
                string payload = System.Text.Encoding.UTF8.GetString(eventArgs.ApplicationMessage.PayloadSegment);
                Console.WriteLine($"Message received from {deviceId}: {payload}");
                _lastPingTime[deviceId] = DateTime.Now;

                // Zapisz do pliku
                File.AppendAllText(_logFilePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {deviceId}: {payload}\n");
            }
            return Task.CompletedTask;
        }

        private async Task MonitoringLoopAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var currentTime = DateTime.Now;
                foreach (var device in _devices)
                {
                    if (_lastPingTime.TryGetValue(device, out var lastPing) && (currentTime - lastPing).TotalSeconds > 2 * _pingInterval)
                    {
                        Console.WriteLine($"{device} not available");
                    }
                    else
                    {
                        Console.WriteLine($"{device} is available");
                    }
                }

                await SendRequestAsync();
                await Task.Delay(_pingInterval * 1000, cancellationToken);
            }
        }

        private async Task SendRequestAsync()
        {
            foreach (var device in _devices)
            {
                var message = new MqttApplicationMessageBuilder()
                    .WithTopic($"request/{device}")
                    .WithPayload("request_data")
                    .Build();

                await _mqttClient.PublishAsync(message);
                Console.WriteLine($"Request sent to {device}");
            }
        }

        public void Dispose()
        {
            StopAsync().Wait();
        }
    }
}