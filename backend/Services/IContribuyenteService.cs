using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DTOs;

namespace backend.Services
{
    public interface IContribuyenteService
    {
        Task<List<ContribuyenteDto>> GetAllContribuyentesAsync();
        Task<ContribuyenteDetalleDto?> GetContribuyenteDetalleAsync(string rncCedula);
    }
}