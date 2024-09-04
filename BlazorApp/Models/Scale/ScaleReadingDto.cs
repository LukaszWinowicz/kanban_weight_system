using BlazorApp.Models.Reading;

namespace BlazorApp.Models.Scale
{
    public class ScaleReadingDto
    {
        public string ScaleName { get; set; }
        public string ItemName { get; set; }
        public decimal SingleItemWeight { get; set; }
        public decimal Value { get; set; }
        public bool IsConnected { get; set; }
        public decimal? Quantity { get; set; }
        public DateTime? ReadingDate { get; set; }
    }
}
