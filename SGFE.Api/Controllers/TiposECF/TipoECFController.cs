using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGFE.Application.Interfaces.TiposECF;
using SGFE.Application.Models.TiposECF;

namespace SGFE.Api.Controllers.TiposECF
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoECFController : ControllerBase
    {
        private readonly ITipoECFService _service;

        public TipoECFController(ITipoECFService service)
        {
            _service = service;
        }


        [Authorize(Roles = "Administrador")]
        [HttpPost("CreateTipo(e-CF)Async")]
        public async Task<IActionResult> CreateTipoECFAsync([FromBody] CreateTipoECFModel model)
        {
            var result = await _service.CrearTiposECFAsync(model);
            if (result == null)
            {
                return NotFound(new
                {
                    message = "No se pudo crear el tipo e-CF con el código: " + model.Codigo
                });
            }
            return Ok(result);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("GetAllTiposECFAsync")]
        public async Task<IActionResult> GetAllTiposECFAsync()
        {
            var result = await _service.GetAllTiposECFAsync();
            if (result == null)
            {
                return NotFound(new
                {
                    message = "No se encontraron tipos e-CF"
                });
            }
            return Ok(result);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("GetTipoECFByIdAsync/{id}")]
        public async Task<IActionResult> GetTipoECFByIdAsync(int id) 
        {
            var result = await _service.GetTipoECFByIdAsync(id);
            if (result == null)
            {
                return NotFound(new
                {
                    message = $"No se encontró un tipo e-CF con Id: {id}"
                });
            }
            return Ok(result);
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeleteTipoECFAsync/{id}")]
        public async Task<IActionResult> DeleteTipoECFAsync(int id)
        {
            var result = await _service.DeleteTipoECFAsync(id);
            if (result == null)
            {
                return NotFound(new
                {
                    message = $"No se encontró un tipo e-CF con Id: {id}"
                });
            }
            return Ok(result);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("UpdateTipoECFAsync")]
        public async Task<IActionResult> UpdateTipoECFAsync([FromBody] UpdateTipoECFModel model)
        {
            var result = await _service.UpdateTipoECFAsync(model);
            if (result == null)
            {
                return NotFound(new
                {
                    message = $"No se pudo actualizar el tipo e-CF con Id: {model.Id}"
                });
            }
            return Ok(result);
        }
    }
}
