using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Controllers;
using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace backend.Tests.Controllers
{
    public class ContribuyentesControllerTests
    {
        private readonly Mock<IContribuyenteService> _mockService;
        private readonly Mock<ILogger<ContribuyentesController>> _mockLogger;
        private readonly ContribuyentesController _controller;

        public ContribuyentesControllerTests()
        {
            _mockService = new Mock<IContribuyenteService>();
            _mockLogger = new Mock<ILogger<ContribuyentesController>>();
            _controller = new ContribuyentesController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetContribuyentes_DeberiaRetornarOkConListaDeContribuyentes()
        {
            var contribuyentesEsperados = new List<ContribuyenteDto>
            {
                new ContribuyenteDto { RncCedula = "98754321012", Nombre = "JUAN PEREZ", Tipo = "PERSONA FISICA", Estatus = "activo" }
            };
            _mockService.Setup(s => s.GetAllContribuyentesAsync()).ReturnsAsync(contribuyentesEsperados);

            var result = await _controller.GetContribuyentes();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var contribuyentes = Assert.IsType<List<ContribuyenteDto>>(okResult.Value);
            Assert.Single(contribuyentes);
            Assert.Equal("JUAN PEREZ", contribuyentes[0].Nombre);
        }

        [Fact]
        public async Task GetContribuyenteDetalle_ConRncValido_DeberiaRetornarOkConDetalle()
        {
            var rncCedula = "98754321012";
            var detalleEsperado = new ContribuyenteDetalleDto
            {
                Contribuyente = new ContribuyenteDto { RncCedula = rncCedula, Nombre = "JUAN PEREZ" },
                ComprobantesFiscales = new List<ComprobanteFiscalDto>(),
                TotalItbis = 216.00m
            };
            _mockService.Setup(s => s.GetContribuyenteDetalleAsync(rncCedula)).ReturnsAsync(detalleEsperado);

            var result = await _controller.GetContribuyenteDetalle(rncCedula);

            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var detalle = Assert.IsType<ContribuyenteDetalleDto>(okResult.Value);
            Assert.Equal(216.00m, detalle.TotalItbis);
            Assert.Equal("JUAN PEREZ", detalle.Contribuyente.Nombre);
        }

        [Fact]
        public async Task GetContribuyenteDetalle_ConRncVacio_DeberiaRetornarBadRequest()
        {
            var result = await _controller.GetContribuyenteDetalle("");

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
        }

        [Fact]
        public async Task GetContribuyenteDetalle_ConRncInexistente_DeberiaRetornarNotFound()
        {
            var rncInexistente = "999999999";
            _mockService.Setup(s => s.GetContribuyenteDetalleAsync(rncInexistente)).ReturnsAsync((ContribuyenteDetalleDto?)null);

            var result = await _controller.GetContribuyenteDetalle(rncInexistente);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.NotNull(notFoundResult.Value);
        }

        [Fact]
        public async Task GetContribuyentes_CuandoOcurreExcepcion_DeberiaRetornar500()
        {
            _mockService.Setup(s => s.GetAllContribuyentesAsync()).ThrowsAsync(new Exception("Error simulado"));

            var result = await _controller.GetContribuyentes();

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}