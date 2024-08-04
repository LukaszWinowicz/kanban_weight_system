using MauiDashboardApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDashboardApp.Models
{
    public class SensorReadingsViewModel : BindableObject
    {
        private readonly ApiService _apiService;
        private ObservableCollection<SensorReading> _sensorReadings;

        public ObservableCollection<SensorReading> SensorReadings
        {
            get => _sensorReadings;
            set
            {
                _sensorReadings = value;
                OnPropertyChanged();
            }
        }

        public SensorReadingsViewModel(ApiService apiService)
        {
            _apiService = apiService;
            SensorReadings = new ObservableCollection<SensorReading>();
        }

        public async Task LoadSensorReadingsAsync()
        {
            try
            {
                var readings = await _apiService.GetSensorReadingsAsync();
                Debug.WriteLine($"Received {readings.Count} readings from API");

                SensorReadings.Clear();
                foreach (var reading in readings)
                {
                    SensorReadings.Add(reading);
                    Debug.WriteLine($"Added reading: {reading.EspName}, Value: {reading.Value}");
                }

                Debug.WriteLine($"Total readings in SensorReadings: {SensorReadings.Count}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in LoadSensorReadingsAsync: {ex.Message}");
            }
        }
    }
}
