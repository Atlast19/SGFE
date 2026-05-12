using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Administrador")]
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


    }
}
