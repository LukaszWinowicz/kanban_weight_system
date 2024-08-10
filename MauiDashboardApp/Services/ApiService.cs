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
        private readonly string _baseUrl = "http://localhost:5000"; // Zakładając, że API działa na tym samym komputerze

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ScaleWithAllReadingsDto>> GetAllScalesWithReadingsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/scale/test");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<ScaleWithAllReadingsDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
