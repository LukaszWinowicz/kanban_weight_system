namespace ApiServer.Core.Entities
{
    public class ScaleEntity
    {
        public string ScaleName { get; set; }  // Klucz główny
        public string ItemName { get; set; }
        public decimal SingleItemWeight { get; set; }
        public bool IsConnected { get; set; }
        public ICollection<ReadingEntity> Readings { get; set; }
    }
}
