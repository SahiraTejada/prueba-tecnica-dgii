using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DTOs;
using backend.Repositories;
using backend.Services;

namespace backend.Services
{
    public class ComprobanteFiscalService : IComprobanteFiscalService
    {
        private readonly IComprobanteFiscalRepository _repository;
        private readonly ILogger<ComprobanteFiscalService> _logger;

        public ComprobanteFiscalService(IComprobanteFiscalRepository repository, ILogger<ComprobanteFiscalService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<ComprobanteFiscalDto>> GetAllComprobantesFiscalesAsync()
        {
            try
            {
                _logger.LogInformation("Procesando solicitud para obtener todos los comprobantes fiscales");
                var comprobantes = await _repository.GetAllAsync();

                return comprobantes.Select(c => new ComprobanteFiscalDto
                {
                    RncCedula = c.RncCedula,
                    NCF = c.NCF,
                    Monto = c.Monto.ToString("F2"),
                    Itbis18 = c.Itbis18.ToString("F2")
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el servicio al obtener comprobantes fiscales");
                throw;
            }
        }

        public async Task<List<ComprobanteFiscalDto>> GetComprobantesByRncCedulaAsync(string rncCedula)
        {
            try
            {
                _logger.LogInformation("Procesando solicitud de comprobantes para: {RncCedula}", rncCedula);
                var comprobantes = await _repository.GetByRncCedulaAsync(rncCedula);

                return comprobantes.Select(c => new ComprobanteFiscalDto
                {
                    RncCedula = c.RncCedula,
                    NCF = c.NCF,
                    Monto = c.Monto.ToString("F2"),
                    Itbis18 = c.Itbis18.ToString("F2")
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el servicio al obtener comprobantes para {RncCedula}", rncCedula);
                throw;
            }
        }
    }
}