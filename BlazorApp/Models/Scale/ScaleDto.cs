using BlazorApp.Models.Reading;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Scale
{
    public class ScaleDto
    {
        public string ScaleName { get; set; }
        public string ItemName { get; set; }
        public decimal SingleItemWeight { get; set; }
        public ICollection<ReadingDto> Readings { get; set; }
    }
}
