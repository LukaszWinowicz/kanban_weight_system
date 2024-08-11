using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using MauiDashboardApp.Models;


namespace MauiDashboardApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5000/api"; // Zakładając, że API działa na tym samym komputerze

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ReadingDto>> GetReadingsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Readings");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<ReadingDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
