using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGFE.Application.Interfaces.Reportes;
using SGFE.Application.Models.Repostes;

namespace SGFE.Api.Controllers.Reportes
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteService _service;

        public ReporteController(IReporteService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("GetFacturaRepostesAsync")]
        public async Task<IActionResult> GetFacturaRepostes([FromQuery] GetFacturaReporteFiltroModel filtroModel)
        {
            var result = await _service.GetFacturaRepostesAsync(filtroModel);
            if (result == null)
            {
                return NotFound(new
                {
                    message = "No se encontraron reportes de facturas con los filtros proporcionados"
                });
            }
            return Ok(result);
        }

    }
}
