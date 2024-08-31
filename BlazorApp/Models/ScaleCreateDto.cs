namespace BlazorApp.Models
{
    public class ScaleCreateDto
    {
        public string ScaleName { get; set; }
        public string ItemName { get; set; }
        public decimal SingleItemWeight { get; set; }
        public bool IsConnected { get; set; }
    }
}
