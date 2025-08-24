using System;
using System.Threading.Tasks;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComprobantesFiscalesController : ControllerBase
    {
        private readonly IComprobanteFiscalService _service;
        private readonly ILogger<ComprobantesFiscalesController> _logger;

        public ComprobantesFiscalesController(IComprobanteFiscalService service, ILogger<ComprobantesFiscalesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los comprobantes fiscales
        /// </summary>
        /// <returns>Lista de comprobantes fiscales</returns>
        [HttpGet]
        public async Task<IActionResult> GetComprobantesFiscales()
        {
            try
            {
                _logger.LogInformation("Solicitud GET recibida para obtener todos los comprobantes fiscales");
                var comprobantes = await _service.GetAllComprobantesFiscalesAsync();
                return Ok(comprobantes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el controlador al obtener comprobantes fiscales");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}