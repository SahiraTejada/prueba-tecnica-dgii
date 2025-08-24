using System;
using System.Threading.Tasks;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContribuyentesController : ControllerBase
    {
        private readonly IContribuyenteService _service;
        private readonly ILogger<ContribuyentesController> _logger;

        public ContribuyentesController(IContribuyenteService service, ILogger<ContribuyentesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los contribuyentes
        /// </summary>
        /// <returns>Lista de contribuyentes</returns>
        [HttpGet]
        public async Task<IActionResult> GetContribuyentes()
        {
            try
            {
                _logger.LogInformation("Solicitud GET recibida para obtener todos los contribuyentes");
                var contribuyentes = await _service.GetAllContribuyentesAsync();
                return Ok(contribuyentes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el controlador al obtener contribuyentes");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Obtiene el detalle de un contribuyente específico con sus comprobantes
        /// </summary>
        /// <param name="rncCedula">RNC o Cédula del contribuyente</param>
        /// <returns>Detalle del contribuyente con comprobantes fiscales</returns>
        [HttpGet("{rncCedula}")]
        public async Task<IActionResult> GetContribuyenteDetalle(string rncCedula)
        {
            try
            {
                _logger.LogInformation("Solicitud GET recibida para obtener detalle del contribuyente: {RncCedula}", rncCedula);

                if (string.IsNullOrEmpty(rncCedula))
                {
                    return BadRequest("RNC/Cédula es requerido");
                }

                var detalle = await _service.GetContribuyenteDetalleAsync(rncCedula);

                if (detalle == null)
                {
                    return NotFound($"Contribuyente con RNC/Cédula {rncCedula} no encontrado");
                }

                return Ok(detalle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el controlador al obtener detalle del contribuyente {RncCedula}", rncCedula);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}