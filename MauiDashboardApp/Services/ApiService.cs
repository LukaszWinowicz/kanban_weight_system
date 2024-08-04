using System.Diagnostics;
using System.Net.Http.Json;
using MauiDashboardApp.Models;

namespace MauiDashboardApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/api/SensorReadings/all")  // Adjust this URL as needed
            };
        }

        public async Task<List<SensorReading>> GetSensorReadingsAsync()
        {
            try
            {
                Debug.WriteLine("Attempting to fetch sensor readings from API");
                var response = await _httpClient.GetAsync("api/SensorReadings");

                if (response.IsSuccessStatusCode)
                {
                    var readings = await response.Content.ReadFromJsonAsync<List<SensorReading>>();
                    Debug.WriteLine($"Successfully fetched {readings?.Count ?? 0} readings from API");
                    return readings ?? new List<SensorReading>();
                }
                else
                {
                    Debug.WriteLine($"API request failed with status code: {response.StatusCode}");
                    return new List<SensorReading>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in GetSensorReadingsAsync: {ex.Message}");
                return new List<SensorReading>();
            }
        }
    }
}