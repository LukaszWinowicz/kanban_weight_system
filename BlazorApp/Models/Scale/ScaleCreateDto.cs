using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Scale
{
    public class ScaleCreateDto
    {
        [Required(ErrorMessage = "Nazwa wagi jest wymagana")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Nazwa musi się zawierać między 6 a 50 znakami.")]
        public string ScaleName { get; set; }

        [Required(ErrorMessage = "Nazwa itemu jest wymagana")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Nazwa musi się zawierać między 6 a 50 znakami.")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Waga pojedynczego przedmiotu jest wymagana.")]
        [Range(0.01, 1000.00, ErrorMessage = "Waga pojedynczego przedmiotu musi być między 0.01 a 1000.00.")]
        public decimal SingleItemWeight { get; set; }
    }
}
