using ApiServer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServer.Core.DTOs
{
    public class ScaleReadingDto
    {
        public int ScaleId { get; set; }
        public string ScaleName { get; set; }
        public string ItemName { get; set; }
        public decimal SingleItemWeight { get; set; }
        public bool IsConnected { get; set; }
        public ReadingEntity LatestReading { get; set; }
        public decimal? Quantity { get; set; }
    }
}
