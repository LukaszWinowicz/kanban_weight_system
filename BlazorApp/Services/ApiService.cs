using BlazorApp.Models.Scale;
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

        // TO: ScaleView
        public async Task<IEnumerable<ScaleReadingDto>> GetLatestReadingForEveryScaleAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Readings/latest");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<IEnumerable<ScaleReadingDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return json;
        }

        // TO: ScaleView
        public async Task<IList<ScaleDto>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Scale/all");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<IList<ScaleDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return json;
        }

        // TO: DatabaseView
        public async Task<IList<ScaleDto>> GetScalesWithAnyReadingsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Scale/withreadings");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<IList<ScaleDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return json;
        }

        // TO: DatabaseView
        public async Task<IList<ScaleReadingDto>> GetAllReadingsByScaleNameAsync(string scaleName)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Readings/getByScaleName/{scaleName}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<IList<ScaleReadingDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return json;
        }

    }
}
