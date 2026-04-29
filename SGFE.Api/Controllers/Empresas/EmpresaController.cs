using Microsoft.AspNetCore.Mvc;
using SGFE.Application.Interfaces.Empresas;
using SGFE.Application.Models.Empresas;

namespace SGFE.Api.Controllers.Empresas
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _service;

        public EmpresaController(IEmpresaService service)
        {
            _service = service;
        }

        [HttpPost("CreateEmpresaAsync")]
        public async Task<IActionResult> CreateEmpresaAsync([FromBody] CreateEmpresaModel model)
        {
            var result = await _service.CreateEmpresaAsync(model);

            if (result == null) 
            {
                return NotFound(new
                {
                    message = "No se pudo crear la empresa con RNC: " + model.RNC
                });
            }

            return Ok(result);
        }

        [HttpGet("GetAllEmpresasAsync")]
        public async Task<IActionResult> GetAllEmpresasAsync()
        {
            var result = await _service.GetAllEmpresaAsync();

            if (result == null)
            {
                return NotFound(new
                {
                    message = "No se encontraron empresas en la base de datos"
                });
            }

            return Ok(result);
        }

        [HttpGet("GetEmpresaByIdAsync/{id}")]
        public async Task<IActionResult> GetEmpresaByIdAsync(int id)
        {
            var result = await _service.GetEmpresaByIdAsync(id);
            if (result == null)
            {
                return NotFound(new 
                {
                    message = $"No se encontró la empresa con ID: {id}"
                });
            }
            return Ok(result);
        }


        [HttpDelete("DeleteEmpresaAsync/{id}")]
        public async Task<IActionResult> DeleteEmpresaAsync(int id)
        { 
            var result = await _service.DeleteEmpresaAsync(id);

            if (result == null) 
            {
                return NotFound(new 
                {
                    message = $"No se puedo desactivar la empresa con ID: {id}"
                });
            }
            return Ok(result);
        }

        [HttpPut("UpdateEmpresaAsync")]
        public async Task<IActionResult> UpdateEmpresaAsync([FromBody] UpdateEmpresaModel model)
        {
            var result = await _service.UpdateEmpresaAsync(model);

            if (result == null)
            {
                return NotFound(new 
                {
                    message = $"No se pudo actualizar la empresa con ID: {model.Id}"
                });
            }
            return Ok(result);
        }
    }
}
