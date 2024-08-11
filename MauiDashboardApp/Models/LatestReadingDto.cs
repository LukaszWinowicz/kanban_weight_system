using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDashboardApp.Models
{
    public class LatestReadingDto
    {
        public int ReadId { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
