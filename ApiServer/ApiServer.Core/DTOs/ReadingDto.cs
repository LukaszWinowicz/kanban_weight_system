namespace ApiServer.Core.DTOs
{
    public class ReadingDto
    {
        public int ReadId { get; set; }
        public DateTime Date { get; set; }
        public string ScaleName { get; set; }  // Klucz obcy
        public decimal Value { get; set; }
        public ScaleDto Scale { get; set; }
    }
}
