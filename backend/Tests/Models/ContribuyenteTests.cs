using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using backend.Models;
using Xunit;

namespace backend.Tests.Models
{
    public class ContribuyenteTests
    {
        [Fact]
        public void Contribuyente_ConDatosValidos_DeberiaSerValido()
        {
            var contribuyente = new Contribuyente
            {
                RncCedula = "98754321012",
                Nombre = "JUAN PEREZ",
                Tipo = "PERSONA FISICA",
                Estatus = "activo"
            };

            var validationResults = ValidateModel(contribuyente);

            Assert.Empty(validationResults);
        }

        [Theory]
        [InlineData("", "Nombre válido", "PERSONA FISICA", "activo")]
        [InlineData("98754321012", "", "PERSONA FISICA", "activo")]
        [InlineData("98754321012", "Nombre válido", "", "activo")]
        [InlineData("98754321012", "Nombre válido", "PERSONA FISICA", "")]
        public void Contribuyente_ConCamposRequeridos_DeberiaFallarValidacion(
            string rncCedula, string nombre, string tipo, string estatus)
        {
            var contribuyente = new Contribuyente
            {
                RncCedula = rncCedula,
                Nombre = nombre,
                Tipo = tipo,
                Estatus = estatus
            };

            var validationResults = ValidateModel(contribuyente);

            Assert.NotEmpty(validationResults);
        }

        [Fact]
        public void Contribuyente_PropiedadesPorDefecto_DeberianSerStringVacio()
        {
            var contribuyente = new Contribuyente();

            Assert.Equal(string.Empty, contribuyente.RncCedula);
            Assert.Equal(string.Empty, contribuyente.Nombre);
            Assert.Equal(string.Empty, contribuyente.Tipo);
            Assert.Equal(string.Empty, contribuyente.Estatus);
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