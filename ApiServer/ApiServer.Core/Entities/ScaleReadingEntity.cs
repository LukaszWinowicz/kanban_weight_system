namespace ApiServer.Core.Entities
{
    public class ScaleReadingEntity
    {
        public string ScaleName { get; set; }
        public string ItemName { get; set; }
        public decimal SingleItemWeight { get; set; }
        public decimal? Value { get; set; }
        public bool IsConnected { get; set; }
        public ReadingEntity? Reading { get; set; }
    }
}
