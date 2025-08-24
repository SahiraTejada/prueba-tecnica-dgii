using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using backend.Repositories;

namespace backend.Repositories
{
    public class ContribuyenteRepository : IContribuyenteRepository
    {
        private readonly ILogger<ContribuyenteRepository> _logger;
        private readonly List<Contribuyente> _contribuyentes;

        public ContribuyenteRepository(ILogger<ContribuyenteRepository> logger)
        {
            _logger = logger;
            _contribuyentes = InitializeData();
        }

        public async Task<List<Contribuyente>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Obteniendo todos los contribuyentes");
                return await Task.FromResult(_contribuyentes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener contribuyentes en el repositorio");
                throw;
            }
        }

        public async Task<Contribuyente?> GetByRncCedulaAsync(string rncCedula)
        {
            try
            {
                _logger.LogInformation("Buscando contribuyente con RNC/Cédula: {RncCedula}", rncCedula);
                return await Task.FromResult(_contribuyentes.FirstOrDefault(c => c.RncCedula == rncCedula));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar contribuyente {RncCedula} en el repositorio", rncCedula);
                throw;
            }
        }

        private List<Contribuyente> InitializeData()
        {
            return new List<Contribuyente>
            {
                new Contribuyente
                {
                    RncCedula = "98754321012",
                    Nombre = "JUAN PEREZ",
                    Tipo = "PERSONA FISICA",
                    Estatus = "activo"
                },
                new Contribuyente
                {
                    RncCedula = "123456789",
                    Nombre = "FARMACIA TU SALUD",
                    Tipo = "PERSONA JURIDICA",
                    Estatus = "inactivo"
                },
                new Contribuyente
                {
                    RncCedula = "987654321",
                    Nombre = "MARIA RODRIGUEZ",
                    Tipo = "PERSONA FISICA",
                    Estatus = "activo"
                },
                new Contribuyente
                {
                    RncCedula = "456789123",
                    Nombre = "SUPERMERCADO LA ECONOMIA",
                    Tipo = "PERSONA JURIDICA",
                    Estatus = "activo"
                }
            };
        }
    }
}