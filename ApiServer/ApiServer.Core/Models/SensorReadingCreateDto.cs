using System.ComponentModel.DataAnnotations;

namespace ApiServer.Core.Models;

    public class SensorReadingCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string EspName { get; set; }

        [Required]
        [MaxLength(50)]
        public string EspId { get; set; }

        [Required]
        public int Value { get; set; }
    }

