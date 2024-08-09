using System.ComponentModel.DataAnnotations;

namespace ApiServer.Core.Entities
{
    public class ReadingEntity
    {
        [Key]
        public int ReadId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]        
        public int ScaleId { get; set; }
        [Required]
        public decimal Value { get; set; }

    }
}
