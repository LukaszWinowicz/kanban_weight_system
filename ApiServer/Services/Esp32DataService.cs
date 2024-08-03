using MQTTnet;
using MQTTnet.Client;

namespace ApiServer.Services
{
    public class Esp32DataService : IDisposable
    {
        private readonly IMqttClient _mqttClient;
        private readonly MqttClientOptions _mqttOptions;
        private readonly string _logFilePath;
        private readonly Dictionary<string, DateTime> _lastPingTime;
        private readonly int _pingInterval = 10; // sekundy
        private readonly string[] _devices = { "esp32_c3_01", "esp32_c3_02" };
        private CancellationTokenSource _cts;

        public Esp32DataService(string mqttServer, string mqttUser, string mqttPass)
        {
            var mqttFactory = new MqttFactory();
            _mqttClient = mqttFactory.CreateMqttClient();

            _mqttOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(mqttServer, 1883)
                .WithCredentials(mqttUser, mqttPass)
                .Build();

            _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "esp32_data.txt");
            _lastPingTime = new Dictionary<string, DateTime>();
        }

        public async Task StartAsync()
        {
            _cts = new CancellationTokenSource();

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
            if (Array.IndexOf(_devices, deviceId) != -1)
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
}
