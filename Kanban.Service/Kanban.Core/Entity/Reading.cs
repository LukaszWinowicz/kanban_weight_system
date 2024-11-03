namespace Kanban.Core.Entity
{
    public class Reading
    {
        public int ReadingId { get; set; }
        public string ScaleName { get; set; }
        public float ReadingWeight { get; set; }
        public DateTime ReadingDate { get; set; }

    }
}
