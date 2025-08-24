using System.Collections.Generic;

namespace backend.DTOs
{
    public class ContribuyenteDetalleDto
    {
        public ContribuyenteDto Contribuyente { get; set; } = new ContribuyenteDto();
        public List<ComprobanteFiscalDto> ComprobantesFiscales { get; set; } = new List<ComprobanteFiscalDto>();
        public decimal TotalItbis { get; set; }
    }
}