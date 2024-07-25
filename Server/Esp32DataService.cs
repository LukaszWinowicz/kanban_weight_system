using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;

namespace Server
{
    public class Esp32DataService : IDisposable
    {
        private readonly IMqttClient _mqttClient;
        private readonly MqttClientOptions _mqttOptions;
        private readonly string _logFilePath;
        private CancellationTokenSource _cts;

        public Esp32DataService(string mqttServer, int mqttPort, string mqttUser, string mqttPass)
        {
            var mqttFactory = new MqttFactory();
            _mqttClient = mqttFactory.CreateMqttClient();

            _mqttOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(mqttServer, mqttPort)
                .WithCredentials(mqttUser, mqttPass)
                .Build();

            _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "esp32_data.txt");
        }

        public async Task StartListeningAsync()
        {
            _cts = new CancellationTokenSource();

            _mqttClient.ConnectedAsync += HandleConnectedAsync;
            _mqttClient.ApplicationMessageReceivedAsync += HandleMessageReceivedAsync;

            await ConnectAsync(_cts.Token);
        }

        public async Task StopListeningAsync()
        {
            _cts?.Cancel();
            await _mqttClient.DisconnectAsync();
        }

        private async Task ConnectAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _mqttClient.ConnectAsync(_mqttOptions, cancellationToken);
                Console.WriteLine("Connected to MQTT broker");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to connect to MQTT broker: {ex.Message}");
            }
        }

        private Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
        {
            Console.WriteLine("Connected to MQTT broker");
            _mqttClient.SubscribeAsync("esp32/data"); // Załóżmy, że ESP32 publikuje na ten temat
            return Task.CompletedTask;
        }

        private Task HandleMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            string payload = System.Text.Encoding.UTF8.GetString(eventArgs.ApplicationMessage.PayloadSegment);
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {payload}";

            Console.WriteLine($"Received: {logEntry}");

            File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            StopListeningAsync().Wait();
        }
    }
}