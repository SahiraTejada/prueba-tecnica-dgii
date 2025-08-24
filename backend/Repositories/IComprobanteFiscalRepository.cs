using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Repositories
{
    public interface IComprobanteFiscalRepository
    {
        Task<List<ComprobanteFiscal>> GetAllAsync();
        Task<List<ComprobanteFiscal>> GetByRncCedulaAsync(string rncCedula);
    }
}