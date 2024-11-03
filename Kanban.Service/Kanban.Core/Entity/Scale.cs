using System;
using System.Collections.Generic;
namespace Kanban.Core.Entity
{
    public class Scale
    {
        public int ScaleId { get; set; }
        public float CalibrationFactor { get; set; }
        public float InitialWeight { get; set; }
    }
}
