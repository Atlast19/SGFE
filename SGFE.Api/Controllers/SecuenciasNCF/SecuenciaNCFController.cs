using Microsoft.AspNetCore.Mvc;
using SGFE.Application.Interfaces.SecuenciasNCF;
using SGFE.Application.Models.SecuenciaNCF;

namespace SGFE.Api.Controllers.SecuenciasNCF
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecuenciaNCFController : ControllerBase
    {
        private readonly ISecuenciaNCFService _service;

        public SecuenciaNCFController(ISecuenciaNCFService service)
        {
            _service = service;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarSecuenciaNCFModel request)
        {
            try
            {
                await _service.RegistrarAsync(request);

                return Ok(new { mensaje = "Secuencia registrada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("next")]
        public async Task<IActionResult> GetNextSecuenciaNCF([FromQuery] CreateSecuenciaNCFModel request)
        {
            try
            {
                var result = await _service.GetNextSecuenciaNCFAsync(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = $"Error al obtener la siguiente secuencia NCF: {ex.Message}"
                });
            }
        }
    }
}
