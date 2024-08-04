using System.Diagnostics;
using MauiDashboardApp.Models;

namespace MauiDashboardApp
{
    public partial class SensorReadingsPage : ContentPage
    {
        private readonly SensorReadingsViewModel _viewModel;

        public SensorReadingsPage(SensorReadingsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        private async void OnLoadClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Load button clicked");
            await _viewModel.LoadSensorReadingsAsync();
            Debug.WriteLine($"After loading, SensorReadings count: {_viewModel.SensorReadings.Count}");
        }
    }
}