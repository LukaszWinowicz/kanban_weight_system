namespace ApiServer.Core.Entities
{
    public class ScaleReadingEntity
    {
        public string ScaleName { get; set; }
        public string ItemName { get; set; }
        public decimal SingleItemWeight { get; set; }
        public bool IsConnected { get; set; }
        public ReadingEntity? LatestReading { get; set; }
    }
}
