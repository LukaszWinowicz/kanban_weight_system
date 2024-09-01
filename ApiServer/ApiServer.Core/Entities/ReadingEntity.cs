namespace ApiServer.Core.Entities
{
    public class ReadingEntity
    {
        public int ReadId { get; set; }
        public DateTime Date { get; set; }
        public string ScaleName { get; set; }  // Klucz obcy
        public decimal Value { get; set; }
        public ScaleEntity Scale { get; set; }
    }
}
