using System.Collections.Generic;
using backend.DTOs;
using Xunit;

namespace backend.Tests.DTOs
{
    public class DtoTests
    {
        [Fact]
        public void ContribuyenteDto_InicializacionPorDefecto_DeberiaInicializarPropiedadesCorrectamente()
        {
            var dto = new ContribuyenteDto();

            Assert.Equal(string.Empty, dto.RncCedula);
            Assert.Equal(string.Empty, dto.Nombre);
            Assert.Equal(string.Empty, dto.Tipo);
            Assert.Equal(string.Empty, dto.Estatus);
        }

        [Fact]
        public void ComprobanteFiscalDto_InicializacionPorDefecto_DeberiaInicializarPropiedadesCorrectamente()
        {
            var dto = new ComprobanteFiscalDto();

            Assert.Equal(string.Empty, dto.RncCedula);
            Assert.Equal(string.Empty, dto.NCF);
            Assert.Equal(string.Empty, dto.Monto);
            Assert.Equal(string.Empty, dto.Itbis18);
        }

        [Fact]
        public void ContribuyenteDetalleDto_InicializacionPorDefecto_DeberiaInicializarObjetosAnidados()
        {
            var dto = new ContribuyenteDetalleDto();

            Assert.NotNull(dto.Contribuyente);
            Assert.NotNull(dto.ComprobantesFiscales);
            Assert.Empty(dto.ComprobantesFiscales);
            Assert.Equal(0m, dto.TotalItbis);
        }

        [Fact]
        public void ContribuyenteDetalleDto_ConDatosCompletos_DeberiaAsignarValoresCorrectamente()
        {
            var dto = new ContribuyenteDetalleDto
            {
                Contribuyente = new ContribuyenteDto
                {
                    RncCedula = "98754321012",
                    Nombre = "JUAN PEREZ"
                },
                ComprobantesFiscales = new List<ComprobanteFiscalDto>
                {
                    new ComprobanteFiscalDto { NCF = "E310000000001", Monto = "200.00", Itbis18 = "36.00" }
                },
                TotalItbis = 36.00m
            };

            Assert.Equal("98754321012", dto.Contribuyente.RncCedula);
            Assert.Equal("JUAN PEREZ", dto.Contribuyente.Nombre);
            Assert.Single(dto.ComprobantesFiscales);
            Assert.Equal(36.00m, dto.TotalItbis);
        }
    }
}