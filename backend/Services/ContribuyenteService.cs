using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DTOs;
using backend.Repositories;
using backend.Services;

namespace backend.Services
{
    public class ContribuyenteService : IContribuyenteService
    {
        private readonly IContribuyenteRepository _contribuyenteRepository;
        private readonly IComprobanteFiscalRepository _comprobanteFiscalRepository;
        private readonly ILogger<ContribuyenteService> _logger;

        public ContribuyenteService(
            IContribuyenteRepository contribuyenteRepository,
            IComprobanteFiscalRepository comprobanteFiscalRepository,
            ILogger<ContribuyenteService> logger)
        {
            _contribuyenteRepository = contribuyenteRepository;
            _comprobanteFiscalRepository = comprobanteFiscalRepository;
            _logger = logger;
        }

        public async Task<List<ContribuyenteDto>> GetAllContribuyentesAsync()
        {
            try
            {
                _logger.LogInformation("Procesando solicitud para obtener todos los contribuyentes");
                var contribuyentes = await _contribuyenteRepository.GetAllAsync();

                return contribuyentes.Select(c => new ContribuyenteDto
                {
                    RncCedula = c.RncCedula,
                    Nombre = c.Nombre,
                    Tipo = c.Tipo,
                    Estatus = c.Estatus
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el servicio al obtener contribuyentes");
                throw;
            }
        }

        public async Task<ContribuyenteDetalleDto?> GetContribuyenteDetalleAsync(string rncCedula)
        {
            try
            {
                _logger.LogInformation("Procesando solicitud de detalle para contribuyente: {RncCedula}", rncCedula);

                var contribuyente = await _contribuyenteRepository.GetByRncCedulaAsync(rncCedula);
                if (contribuyente == null)
                {
                    _logger.LogWarning("Contribuyente {RncCedula} no encontrado", rncCedula);
                    return null;
                }

                var comprobantes = await _comprobanteFiscalRepository.GetByRncCedulaAsync(rncCedula);
                var totalItbis = comprobantes.Sum(c => c.Itbis18);

                return new ContribuyenteDetalleDto
                {
                    Contribuyente = new ContribuyenteDto
                    {
                        RncCedula = contribuyente.RncCedula,
                        Nombre = contribuyente.Nombre,
                        Tipo = contribuyente.Tipo,
                        Estatus = contribuyente.Estatus
                    },
                    ComprobantesFiscales = comprobantes.Select(c => new ComprobanteFiscalDto
                    {
                        RncCedula = c.RncCedula,
                        NCF = c.NCF,
                        Monto = c.Monto.ToString("F2"),
                        Itbis18 = c.Itbis18.ToString("F2")
                    }).ToList(),
                    TotalItbis = totalItbis
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el servicio al obtener detalle del contribuyente {RncCedula}", rncCedula);
                throw;
            }
        }
    }
}