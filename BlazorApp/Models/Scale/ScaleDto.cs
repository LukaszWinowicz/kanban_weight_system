using BlazorApp.Models.Reading;

namespace BlazorApp.Models.Scale
{
    public class ScaleDto
    {
        public string ScaleName { get; set; }
        public string ItemName { get; set; }
        public decimal SingleItemWeight { get; set; }
        public bool IsConnected { get; set; }
        public ICollection<ReadingDto> Readings { get; set; }
    }
}
