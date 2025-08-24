using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Repositories
{
    public interface IContribuyenteRepository
    {
        Task<List<Contribuyente>> GetAllAsync();
        Task<Contribuyente?> GetByRncCedulaAsync(string rncCedula);
    }
}