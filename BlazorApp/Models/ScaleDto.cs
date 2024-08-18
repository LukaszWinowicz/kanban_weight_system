namespace BlazorApp.Models
{
    public class ScaleDto
    {
        public int ScaleId { get; set; }
        public string ScaleName { get; set; }
        public string ItemName { get; set; }
        public decimal SingleItemWeight { get; set; }
        public bool IsConnected { get; set; }
        public ReadingDto Readings { get; set; }
    }
}
