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
    public class ComprobantesFiscalesControllerTests
    {
        private readonly Mock<IComprobanteFiscalService> _mockService;
        private readonly Mock<ILogger<ComprobantesFiscalesController>> _mockLogger;
        private readonly ComprobantesFiscalesController _controller;

        public ComprobantesFiscalesControllerTests()
        {
            _mockService = new Mock<IComprobanteFiscalService>();
            _mockLogger = new Mock<ILogger<ComprobantesFiscalesController>>();
            _controller = new ComprobantesFiscalesController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetComprobantesFiscales_DeberiaRetornarOkConListaDeComprobantes()
        {
            var comprobantesEsperados = new List<ComprobanteFiscalDto>
            {
                new ComprobanteFiscalDto
                {
                    RncCedula = "98754321012",
                    NCF = "E310000000001",
                    Monto = "200.00",
                    Itbis18 = "36.00"
                }
            };
            _mockService.Setup(s => s.GetAllComprobantesFiscalesAsync()).ReturnsAsync(comprobantesEsperados);

            var result = await _controller.GetComprobantesFiscales();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var comprobantes = Assert.IsType<List<ComprobanteFiscalDto>>(okResult.Value);
            Assert.Single(comprobantes);
            Assert.Equal("E310000000001", comprobantes[0].NCF);
        }

        [Fact]
        public async Task GetComprobantesFiscales_CuandoOcurreExcepcion_DeberiaRetornar500()
        {
            _mockService.Setup(s => s.GetAllComprobantesFiscalesAsync()).ThrowsAsync(new Exception("Error simulado"));

            var result = await _controller.GetComprobantesFiscales();

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}