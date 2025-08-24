using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Contribuyente
    {
        [Required]
        public string RncCedula { get; set; } = string.Empty;

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        public string Estatus { get; set; } = string.Empty;
    }
}