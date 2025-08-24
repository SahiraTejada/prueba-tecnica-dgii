using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DTOs;

namespace backend.Services
{
    public interface IComprobanteFiscalService
    {
        Task<List<ComprobanteFiscalDto>> GetAllComprobantesFiscalesAsync();
        Task<List<ComprobanteFiscalDto>> GetComprobantesByRncCedulaAsync(string rncCedula);
    }
}