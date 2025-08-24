using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DTOs;
using backend.Models;
using backend.Repositories;
using backend.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace backend.Tests.Services
{
    public class ContribuyenteServiceTests
    {
        private readonly Mock<IContribuyenteRepository> _mockContribuyenteRepository;
        private readonly Mock<IComprobanteFiscalRepository> _mockComprobanteFiscalRepository;
        private readonly Mock<ILogger<ContribuyenteService>> _mockLogger;
        private readonly ContribuyenteService _service;

        public ContribuyenteServiceTests()
        {
            _mockContribuyenteRepository = new Mock<IContribuyenteRepository>();
            _mockComprobanteFiscalRepository = new Mock<IComprobanteFiscalRepository>();
            _mockLogger = new Mock<ILogger<ContribuyenteService>>();

            _service = new ContribuyenteService(
                _mockContribuyenteRepository.Object,
                _mockComprobanteFiscalRepository.Object,
                _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllContribuyentesAsync_DeberiaRetornarListaDeContribuyentesDto()
        {
            var contribuyentes = new List<Contribuyente>
            {
                new Contribuyente { RncCedula = "98754321012", Nombre = "JUAN PEREZ", Tipo = "PERSONA FISICA", Estatus = "activo" },
                new Contribuyente { RncCedula = "123456789", Nombre = "FARMACIA TU SALUD", Tipo = "PERSONA JURIDICA", Estatus = "inactivo" }
            };

            _mockContribuyenteRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(contribuyentes);

            var result = await _service.GetAllContribuyentesAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("JUAN PEREZ", result[0].Nombre);
            Assert.Equal("FARMACIA TU SALUD", result[1].Nombre);
        }

        [Fact]
        public async Task GetContribuyenteDetalleAsync_ConDatosCompletos_DeberiaCalcularTotalItbisCorrectamente()
        {
            var rncCedula = "98754321012";
            var contribuyente = new Contribuyente
            {
                RncCedula = rncCedula,
                Nombre = "JUAN PEREZ",
                Tipo = "PERSONA FISICA",
                Estatus = "activo"
            };
            var comprobantes = new List<ComprobanteFiscal>
            {
                new ComprobanteFiscal { RncCedula = rncCedula, NCF = "E310000000001", Monto = 200.00m, Itbis18 = 36.00m },
                new ComprobanteFiscal { RncCedula = rncCedula, NCF = "E310000000002", Monto = 1000.00m, Itbis18 = 180.00m }
            };

            _mockContribuyenteRepository.Setup(r => r.GetByRncCedulaAsync(rncCedula)).ReturnsAsync(contribuyente);
            _mockComprobanteFiscalRepository.Setup(r => r.GetByRncCedulaAsync(rncCedula)).ReturnsAsync(comprobantes);

            var result = await _service.GetContribuyenteDetalleAsync(rncCedula);

            Assert.NotNull(result);
            Assert.Equal(216.00m, result.TotalItbis);
            Assert.Equal(2, result.ComprobantesFiscales.Count);
            Assert.Equal("JUAN PEREZ", result.Contribuyente.Nombre);
        }

        [Fact]
        public async Task GetContribuyenteDetalleAsync_ConRncInexistente_DeberiaRetornarNull()
        {
            var rncInexistente = "999999999";
            _mockContribuyenteRepository.Setup(r => r.GetByRncCedulaAsync(rncInexistente)).ReturnsAsync((Contribuyente?)null);

            var result = await _service.GetContribuyenteDetalleAsync(rncInexistente);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllContribuyentesAsync_CuandoOcurreExcepcion_DeberiaLoguearError()
        {
            var excepcionSimulada = new Exception("Error de conexión simulado");
            _mockContribuyenteRepository.Setup(r => r.GetAllAsync()).ThrowsAsync(excepcionSimulada);

            await Assert.ThrowsAsync<Exception>(() => _service.GetAllContribuyentesAsync());

            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Error en el servicio al obtener contribuyentes")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}