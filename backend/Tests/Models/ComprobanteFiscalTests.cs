using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using backend.Models;
using Xunit;

namespace backend.Tests.Models
{
    public class ComprobanteFiscalTests
    {
        [Fact]
        public void ComprobanteFiscal_ConDatosValidos_DeberiaSerValido()
        {
            var comprobante = new ComprobanteFiscal
            {
                RncCedula = "98754321012",
                NCF = "E310000000001",
                Monto = 200.00m,
                Itbis18 = 36.00m
            };

            var validationResults = ValidateModel(comprobante);

            Assert.Empty(validationResults);
        }

        [Theory]
        [InlineData(-1, 36.00)]
        [InlineData(200.00, -1)]
        [InlineData(-100, -50)]
        public void ComprobanteFiscal_ConMontosNegativos_DeberiaFallarValidacion(
            decimal monto, decimal itbis18)
        {
            var comprobante = new ComprobanteFiscal
            {
                RncCedula = "98754321012",
                NCF = "E310000000001",
                Monto = monto,
                Itbis18 = itbis18
            };

            var validationResults = ValidateModel(comprobante);

            Assert.NotEmpty(validationResults);
        }

        [Fact]
        public void ComprobanteFiscal_CalculoItbis_DeberiaSerConsistente()
        {
            var monto = 1000.00m;
            var itbisEsperado = monto * 0.18m;

            var comprobante = new ComprobanteFiscal
            {
                RncCedula = "98754321012",
                NCF = "E310000000001",
                Monto = monto,
                Itbis18 = itbisEsperado
            };

            Assert.Equal(180.00m, comprobante.Itbis18);
            Assert.Equal(1000.00m, comprobante.Monto);
        }

        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
    }
}