using MauiDashboardApp.Services;

namespace MauiDashboardApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private readonly ApiService _apiService;

        public MainPage(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void OnLoadScalesClicked(object sender, EventArgs e)
        {
            try
            {
                var readings = await _apiService.GetScalesWithLatestReadingsAsync();
                ScalesCollectionView.ItemsSource = readings;
            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", $"Failed to load readings: {ex.Message}", "OK");
            }
        }
        /*private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);*/
        //}

    }

}
