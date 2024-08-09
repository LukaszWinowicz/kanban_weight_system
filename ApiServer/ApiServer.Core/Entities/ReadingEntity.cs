namespace ApiServer.Core.Entities
{
    public class ReadingEntity
    {
        public int ReadId { get; set; }
        public DateTime Date { get; set; }
        public int ScaleId { get; set; }
        public decimal Value { get; set; }
        public ScaleEntity Scale { get; set; }

    }
}
