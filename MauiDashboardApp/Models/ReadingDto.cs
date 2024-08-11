using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDashboardApp.Models
{
    public class ReadingDto
    {
        public int ReadId { get; set; }
        public DateTime Date { get; set; }
        public int Scaleid { get; set; }
        public decimal Value { get; set; }
        public ScaleDto Scale { get; set; }
    }
}
