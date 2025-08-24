using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using backend.Repositories;

namespace backend.Repositories
{
    public class ComprobanteFiscalRepository : IComprobanteFiscalRepository
    {
        private readonly ILogger<ComprobanteFiscalRepository> _logger;
        private readonly List<ComprobanteFiscal> _comprobantes;

        public ComprobanteFiscalRepository(ILogger<ComprobanteFiscalRepository> logger)
        {
            _logger = logger;
            _comprobantes = InitializeData();
        }

        public async Task<List<ComprobanteFiscal>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Obteniendo todos los comprobantes fiscales");
                return await Task.FromResult(_comprobantes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener comprobantes fiscales en el repositorio");
                throw;
            }
        }

        public async Task<List<ComprobanteFiscal>> GetByRncCedulaAsync(string rncCedula)
        {
            try
            {
                _logger.LogInformation("Obteniendo comprobantes para RNC/Cédula: {RncCedula}", rncCedula);
                return await Task.FromResult(_comprobantes.Where(c => c.RncCedula == rncCedula).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener comprobantes para {RncCedula} en el repositorio", rncCedula);
                throw;
            }
        }

        private List<ComprobanteFiscal> InitializeData()
        {
            return new List<ComprobanteFiscal>
            {
                new ComprobanteFiscal
                {
                    RncCedula = "98754321012",
                    NCF = "E310000000001",
                    Monto = 200.00m,
                    Itbis18 = 36.00m
                },
                new ComprobanteFiscal
                {
                    RncCedula = "98754321012",
                    NCF = "E310000000002",
                    Monto = 1000.00m,
                    Itbis18 = 180.00m
                },
                new ComprobanteFiscal
                {
                    RncCedula = "123456789",
                    NCF = "E310000000003",
                    Monto = 500.00m,
                    Itbis18 = 90.00m
                },
                new ComprobanteFiscal
                {
                    RncCedula = "987654321",
                    NCF = "E310000000004",
                    Monto = 300.00m,
                    Itbis18 = 54.00m
                },
                new ComprobanteFiscal
                {
                    RncCedula = "456789123",
                    NCF = "E310000000005",
                    Monto = 750.00m,
                    Itbis18 = 135.00m
                }
            };
        }
    }
}