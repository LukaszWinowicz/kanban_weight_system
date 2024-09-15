namespace ApiServer.Core.DTOs
{
    public class ScaleCreateDto
    {
        public string ScaleName { get; set; }
        public string ItemName { get; set; }
        public decimal SingleItemWeight { get; set; }
    }
}
