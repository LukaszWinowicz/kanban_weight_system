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

        //public async Task<IEnumerable<ScaleWithLatestReadingDto>> GetScalesWithLatestReadingsAsync()
        //{
        //    var response = await _httpClient.GetAsync($"{_baseUrl}/ScaleReadings/all");
        //    response.EnsureSuccessStatusCode();
        //    var content = await response.Content.ReadAsStringAsync();
        //    var json = JsonSerializer.Deserialize<IEnumerable<ScaleWithLatestReadingDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //    return json;
        //}

        public async Task<Result<IEnumerable<ScaleWithLatestReadingDto>>> GetScalesWithLatestReadingsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/ScaleReadings/all");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var json = JsonSerializer.Deserialize<IEnumerable<ScaleWithLatestReadingDto>>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Result<IEnumerable<ScaleWithLatestReadingDto>>.Success(json);
                }
                else
                {
                    return Result<IEnumerable<ScaleWithLatestReadingDto>>.Failure($"Błąd HTTP: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                return Result<IEnumerable<ScaleWithLatestReadingDto>>.Failure($"Błąd połączenia: {ex.Message}");
            }
            catch (JsonException ex)
            {
                return Result<IEnumerable<ScaleWithLatestReadingDto>>.Failure($"Błąd deserializacji JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ScaleWithLatestReadingDto>>.Failure($"Nieoczekiwany błąd: {ex.Message}");
            }
        }

        public async Task<IList<LatestReadingDto>> GetAllReadingsForScale(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/ScaleReadings/{id}/readings");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<IList<LatestReadingDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return json;
        }

        public async Task<IList<ScaleDto>> GetAllScales()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Scale/test");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<IList<ScaleDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return json;
        }

        // Klasa pomocnicza do zwracania wyniku
        public class Result<T>
        {
            public bool IsSuccess { get; private set; }
            public T Value { get; private set; }
            public string Error { get; private set; }

            private Result(bool isSuccess, T value, string error)
            {
                IsSuccess = isSuccess;
                Value = value;
                Error = error;
            }

            public static Result<T> Success(T value) => new Result<T>(true, value, null);
            public static Result<T> Failure(string error) => new Result<T>(false, default, error);
        }

    }
}
