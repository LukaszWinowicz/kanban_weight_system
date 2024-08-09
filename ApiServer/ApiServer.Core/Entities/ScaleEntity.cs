using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServer.Core.Entities
{
    public class ScaleEntity
    {
        public int ScaleId { get; set; }
        public string ScaleName { get; set; }
        public string ItemName { get; set; }
        public decimal SingleItemWeight { get; set; }
        public bool IsConnected { get; set; }
        public string Location { get; set; }
        public DateTime LastCalibrationDate { get; set; }
    }
}
