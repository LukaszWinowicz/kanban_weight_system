using BlazorApp.Models.Scale;

namespace BlazorApp.Models.Reading
{
    public class ReadingDto
    {
        public int ReadId { get; set; }
        public DateTime Date { get; set; }
        public int ScaleId { get; set; }
        public decimal Value { get; set; }
        public ScaleDto Scale { get; set; }
    }
}
