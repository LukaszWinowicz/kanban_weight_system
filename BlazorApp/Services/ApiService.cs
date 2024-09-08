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
        public async Task<bool> ReadNewDataFromScale(string scaleName)
        {
            return false;
        }

        // TO: DatabaseView
        public async Task<IEnumerable<ScaleDto>> GetScalesWithAnyReadingsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Scale/withreadings");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<IEnumerable<ScaleDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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

        // TO: SettingsView
        public async Task<IEnumerable<ScaleDto>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Scale/all");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<IEnumerable<ScaleDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return json;
        }

        // TO: SettingsView
        public async Task<bool> CreateScaleAsync(ScaleCreateDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/Scale", dto);

                if (response.IsSuccessStatusCode)
                {
                    // Zwraca true, jeśli odpowiedź wskazuje na sukces
                    return true;
                }
                else
                {
                    // Wypisuje komunikat błędu, jeśli odpowiedź nie wskazuje na sukces
                    Console.WriteLine($"Error creating scale: {response.ReasonPhrase}");
                    return false;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error creating scale: {e.Message}");
                throw;
            }
        }
        // TO: SettingsView        
        public async Task<bool> DeleteScaleByName(string scaleName)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/Scale/delete/{scaleName}");
                if (response.IsSuccessStatusCode)
                {
                    // Zwraca true, jeśli odpowiedź wskazuje na sukces
                    return true;
                }
                else
                {
                    // Wypisuje komunikat błędu, jeśli odpowiedź nie wskazuje na sukces
                    Console.WriteLine($"Error creating scale: {response.ReasonPhrase}");
                    return false;
                }
            }
            catch (HttpRequestException e)
            {

                Console.WriteLine($"Error delete scale: {e.Message}");
                throw;
            }
        }
    }
}
