using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class ComprobanteFiscal
    {
        [Required]
        public string RncCedula { get; set; } = string.Empty;

        [Required]
        public string NCF { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Monto { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Itbis18 { get; set; }
    }
}