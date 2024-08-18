namespace BlazorApp.Models
{
    public class ScaleReadingDto
    {
        public int ScaleId { get; set; }
        public string ScaleName { get; set; }
        public string ItemName { get; set; }
        public decimal SingleItemWeight { get; set; }
        public bool IsConnected { get; set; }
        public ReadingDto LatestReading { get; set; }

        public decimal? Quantity { get; set; }
    }
}
