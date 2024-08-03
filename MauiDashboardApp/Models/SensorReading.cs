using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDashboardApp.Models
{
    public class SensorReading
    {
        [Key]
        public int SensorId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(50)]
        public string EspName { get; set; }

        [Required]
        [MaxLength(50)]
        public string EspId { get; set; }

        [Required]
        public int Value { get; set; }
    }
}
