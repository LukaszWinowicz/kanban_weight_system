using ApiServer.Core.Interfaces;
using ApiServer.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using MQTTnet;
using MQTTnet.Client;
using System;

namespace ApiServer.WindowsForms.Services
{
    public class Esp32DataService : IDisposable
    {
        private readonly IMqttClient _mqttClient;
        private readonly MqttClientOptions _mqttOptions;
        private readonly Dictionary<string, DateTime> _lastPingTime;
        private readonly int _pingInterval = 60; // sekundy (1 minuta)
        private List<string> _devices;
        private CancellationTokenSource _cts;
        private readonly ApiServerContext _context;

        public Esp32DataService(string mqttServer, string mqttUser, string mqttPass)
        {
            var mqttFactory = new MqttFactory();
            _mqttClient = mqttFactory.CreateMqttClient();

            _mqttOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(mqttServer, 1883)
                .WithCredentials(mqttUser, mqttPass)
                .Build();

            _lastPingTime = new Dictionary<string, DateTime>();

            // Inicjalizacja kontekstu bazy danych
            _context = new ApiServerContext(); // Upewnij się, że ApiServerContext ma odpowiedni konstruktor
        }

        public async Task StartAsync()
        {
            _cts = new CancellationTokenSource();

            // Pobierz listę urządzeń z bazy danych
            var scales = await _context.Scale.ToListAsync(); // Użyj wersji asynchronicznej
            _devices = scales.Select(s => s.ScaleName).ToList();

            _mqttClient.ConnectedAsync += HandleConnectedAsync;
            _mqttClient.ApplicationMessageReceivedAsync += HandleMessageReceivedAsync;

            await _mqttClient.ConnectAsync(_mqttOptions, _cts.Token);
            Console.WriteLine("Connected to MQTT broker");

            // Uruchomienie pętli monitorowania
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

                // Aktualizuj czas odpowiedzi urządzenia
                _lastPingTime[deviceId] = DateTime.Now;
            }
            return Task.CompletedTask;
        }


        private async Task MonitoringLoopAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // Wysyłanie zapytania do każdej wagi
                await SendRequestAsync();

                // Oczekiwanie na odpowiedzi od urządzeń
                await Task.Delay(_pingInterval * 1000, cancellationToken); // Czekaj na odpowiedź wagi przez _pingInterval sekund

                // Sprawdzenie, które urządzenia odpowiedziały
                foreach (var device in _devices)
                {
                    // Jeśli waga odpowiedziała w wymaganym czasie, zostaje zapisana w _lastPingTime
                    if (_lastPingTime.ContainsKey(device))
                    {
                        Console.WriteLine($"{device} is connected: true");
                    }
                    else
                    {
                        Console.WriteLine($"{device} is connected: false");
                    }
                }

                // Wyczyść _lastPingTime, aby przygotować na kolejną rundę monitorowania
                _lastPingTime.Clear();
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
