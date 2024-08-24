using BlazorApp.Models;
using System.Text.Json;

namespace BlazorApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5000/api"; // Zakładając, że API działa na tym samym komputerze

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CheckApiConnection()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Health/test");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ScaleReadingDto>> GetLatestReadingForEveryScaleAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Readings/latest");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<IEnumerable<ScaleReadingDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return json;
        }

        public async Task ReadNewDataFromScale(int scaleId)
        {
            var createDto = new CreateReadingDto
            {
                ScaleId = scaleId
            };

            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/Readings/create", createDto);

            response.EnsureSuccessStatusCode();
        }

        public async Task<IList<ScaleDto>> GetScalesWithAnyReadings()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Scale/withreadings");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<IList<ScaleDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return json;
        }

        public async Task<IList<ScaleDto>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Scale/all");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<IList<ScaleDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return json;
        }

        public async Task<IList<ScaleReadingDto>> GetAllReadingsByScaleId(int scaleId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Readings/getByScaleId/{scaleId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<IList<ScaleReadingDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return json;
        }
    }
}
