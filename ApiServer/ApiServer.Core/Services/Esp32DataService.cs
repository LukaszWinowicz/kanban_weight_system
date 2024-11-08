﻿using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using MQTTnet;
using MQTTnet.Client;
using System.Text;

namespace ApiServer.Core.Services
{
    public class Esp32DataService
    {
        private readonly IScaleRepository _scaleRepository;
        private readonly IReadingsRepository _readingsRepository;
        private readonly IMqttClient _mqttClient; // IMqttClient: Reprezentuje klienta MQTT, który będzie używany do nawiązywania połączeń z brokerem MQTT.
        private readonly MqttClientOptions _mqttOptions; // MqttClientOptions: Ustawia opcje klienta, takie jak ID klienta, adres serwera, port i dane uwierzytelniające.
        private CancellationTokenSource _cancellationTokenSource; // Token do anulowania zadania
        private bool _isPollingActive = false; // Flaga wskazująca, czy odpytywanie jest aktywne

        public Esp32DataService(IScaleRepository scaleRepository, IReadingsRepository readingsRepository) 
        {
            _scaleRepository = scaleRepository;
            _readingsRepository = readingsRepository;

            // Tworzenie fabryki MQTT
            // MqttFactory: Tworzy instancję fabryki klienta MQTT, która umożliwia tworzenie nowych klientów MQTT.
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            // Konfiguracja opcji klienta MQTT
            _mqttOptions = new MqttClientOptionsBuilder()
                .WithClientId("esp32-monitor-server") // Unikalny identyfikator klienta (serwera)
                .WithTcpServer("172.20.10.2", 1883) // Adres i port Twojego brokera MQTT
                .WithCredentials("mqtt_user", "mqtt_pass") // Dane logowania do brokera MQTT
                .Build();
        }

        public IEnumerable<ScaleEntity> ScalesList()
        {
            var entities = _scaleRepository.GetAll();
            return entities;
        }
       
        public bool IsScaleConnected(string scaleName)
        {
            try
            {
                // Połączenie z brokerem MQTT
                _mqttClient.ConnectAsync(_mqttOptions).Wait();

                // TaskCompletionSource do oczekiwania na odpowiedź
                var tcs = new TaskCompletionSource<bool>();

                // Obsługa przychodzących wiadomości MQTT
                Func<MqttApplicationMessageReceivedEventArgs, Task> messageHandler = e =>
                {
                    var topic = e.ApplicationMessage.Topic;
                    var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                    // Sprawdzanie, czy temat wiadomości jest odpowiedzią od ESP32
                    if (topic == $"response/{scaleName}")
                    {
                        if (!tcs.Task.IsCompleted)
                        {
                            tcs.SetResult(true);

                            // Jeśli waga jest podłączona, parsujemy odczyt
                            var reading = ParseReading(payload, scaleName);
                            if (reading != null)
                            {
                                // Zapis do bazy danych
                                _readingsRepository.AddReading(reading);
                                Console.WriteLine($"Odczyt zapisany do bazy danych: {payload}");
                            }
                        }
                    }

                    return Task.CompletedTask;
                };

                // Subskrybuj wiadomości
                _mqttClient.ApplicationMessageReceivedAsync += messageHandler;

                // Subskrypcja na temat odpowiedzi od konkretnego ESP32
                _mqttClient.SubscribeAsync($"response/{scaleName}").Wait();

                // Wysłanie zapytania do konkretnego ESP32
                var message = new MqttApplicationMessageBuilder()
                    .WithTopic($"request/{scaleName}")
                    .WithPayload("check")
                    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce)
                    .WithRetainFlag(false)
                    .Build();

                _mqttClient.PublishAsync(message).Wait();

                // Oczekiwanie na odpowiedź przez maksymalnie 5 sekund
                bool messageReceived = tcs.Task.Wait(5000);

                // Odsubskrybowanie wiadomości po otrzymaniu odpowiedzi lub po upłynięciu czasu
                _mqttClient.ApplicationMessageReceivedAsync -= messageHandler;

                // Rozłączenie się z brokerem MQTT
                _mqttClient.DisconnectAsync().Wait();

                return messageReceived;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas sprawdzania ESP32: {ex.Message}");
                return false;
            }
        }

        private ReadingEntity ParseReading(string payload, string scaleName)
        {
            try
            {
                Console.WriteLine($"Otrzymano payload: {payload}");

                // Spodziewany format: "Urządzenie: ESP32-001, Waga: X.XX gram"
                var parts = payload.Split(new[] { ", " }, StringSplitOptions.None);

                if (parts.Length < 2)
                {
                    Console.WriteLine("Błąd: Nieoczekiwany format danych.");
                    return null;
                }

                var date = DateTime.Now; // Aktualna data i czas odczytu

                // Parsowanie wartości wagi
                var weightPart = parts[1].Split(new[] { ": " }, StringSplitOptions.None);
                if (weightPart.Length < 2)
                {
                    Console.WriteLine("Błąd: Nieoczekiwany format wagi.");
                    return null;
                }

                // Wyodrębnienie liczbowej wartości wagi
                var weightString = weightPart[1].Replace(" gram", "").Trim();
                Console.WriteLine($"Parsowana wartość wagi: {weightString}");

                decimal weight;
                if (!decimal.TryParse(weightString, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out weight))
                {
                    Console.WriteLine("Nieprawidłowy format wartości wagi.");
                    return null;
                }

                return new ReadingEntity
                {
                    Date = date,
                    Value = weight,
                    ScaleName = scaleName
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas parsowania odczytu: {ex.Message}");
                return null;
            }
        }


        //private ReadingEntity ParseReading(string payload, string scaleName)
        //{
        //    try
        //    {
        //        // Payload jest w formacie: "Device: ESP32-001, ID: ESP32_xxxx, Number: X"
        //        var parts = payload.Split(new[] { ", " }, StringSplitOptions.None);

        //        if (parts.Length < 3) return null;

        //        var date = DateTime.Now; // Aktualna data i czas odczytu
        //        var valuePart = parts[2].Split(new[] { ": " }, StringSplitOptions.None);
        //        if (valuePart.Length < 2) return null;

        //        // Zamiana odczytanej wartości na decimal
        //        decimal value;
        //        if (!decimal.TryParse(valuePart[1], out value))
        //        {
        //            Console.WriteLine("Nieprawidłowy format wartości odczytu.");
        //            return null;
        //        }

        //        return new ReadingEntity
        //        {
        //            Date = date,
        //            Value = value,
        //            ScaleName = scaleName
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Błąd podczas parsowania odczytu: {ex.Message}");
        //        return null; // W przypadku błędu zwróć null
        //    }
        //}

        public void StartPollingScales()
        {
            if (_isPollingActive)
            {
                Console.WriteLine("Odpytywanie jest już aktywne.");
                return;
            }

            _isPollingActive = true;

            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;

            Task.Run(async () =>
            {
                var readingsBatch = new List<ReadingEntity>();

                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var scales = ScalesList();

                        foreach (var scale in scales)
                        {
                            string response = GetEsp32Value(scale.ScaleName);

                            if (!string.IsNullOrEmpty(response))
                            {
                                var reading = ParseReading(response, scale.ScaleName);
                                if (reading != null)
                                {
                                    readingsBatch.Add(reading); // Dodaj odczyt do paczki
                                    Console.WriteLine($"Odczyt dodany do paczki: {response}");
                                }
                            }

                            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                        }

                        // Zapisujemy paczkę danych do bazy co 1 minutę
                        if (readingsBatch.Any())
                        {
                            _readingsRepository.AddReadingsBatch(readingsBatch); // Zapisujemy paczkę danych
                            Console.WriteLine("Paczka danych zapisana do bazy.");
                            readingsBatch.Clear(); // Czyścimy paczkę po zapisie
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Błąd podczas odpytywania: {ex.Message}");
                    }

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

                    // Sprawdzenie, czy temat wiadomości jest odpowiedzią od ESP32
                    if (topic == $"response/{deviceNumber}")
                    {
                        if (!tcs.Task.IsCompleted) // Sprawdzenie, czy zadanie nie jest już zakończone
                        {
                            tcs.SetResult(payload); // Ustawienie wyniku na payload wiadomości
                        }
                    }

                    return Task.CompletedTask;
                };

                // Subskrybcja wiadomości
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
