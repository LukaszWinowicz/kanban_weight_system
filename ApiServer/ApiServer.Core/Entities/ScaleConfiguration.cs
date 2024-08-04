using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServer.Core.Entities
{
    public class ScaleConfiguration
    {
        [Key]
        public string EspId { get; set; }

        [Required]
        public int ItemNumber { get; set; }

        [Required]
        public decimal SingleItemWeight { get; set; }

        [Required]
        public bool IsConnected { get; set; }

       /* [MaxLength(100)]
        public string Location { get; set; }

        public DateTime LastCalibrationDate { get; set; }

        [Range(0, 10000)]
        public decimal MaxCapacity { get; set; }

        public decimal Tolerance { get; set; }*/
    }
}
