namespace ApiServer.Core.DTOs
{
    public class ScaleWithAllReadingsDto
    {
        public int ScaleId { get; set; }
        public string ScaleName { get; set; }
        public string ItemName { get; set; }
        public decimal SingleItemWeight { get; set; }
        public bool IsConnected { get; set; }
        public List<ReadingDto> Readings { get; set; }
    }
}
