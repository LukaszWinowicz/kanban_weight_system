﻿using BlazorApp.Models;
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

        public async Task<IEnumerable<ScaleWithLatestReadingDto>> GetScalesWithLatestReadingsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/ScaleReadings/all");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<IEnumerable<ScaleWithLatestReadingDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return json;
        }
    }
}