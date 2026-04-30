using Microsoft.AspNetCore.Mvc;
using SGFE.Application.Interfaces.Clientes;
using SGFE.Application.Models.Clientes;

namespace SGFE.Api.Controllers.Clientes
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        [HttpPost("CreateClieteAsync")]
        public async Task<IActionResult> CreateClienteAsync([FromBody] CreateClienteModel model)
        {
            var result = await _service.CreateClienteAsync(model);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "No se pudo crear el cliente con Documento: " + model.Documento
                });
            }

            return Ok(result);
        }

        [HttpGet("GetAllClientesAsync")]
        public async Task<IActionResult> GetAllClienteAsync()
        {
            var result = await _service.GetAllClienteAsync();

            if (result == null)
            {
                return NotFound(new
                {
                    message = "No se encontraron clientes en la base de datos"
                });
            }

            return Ok(result);
        }

        [HttpGet("GetClieteByIdAsync/{Id}")]
        public async Task<IActionResult> GetClienteByAsync(int Id)
        {
            var result = await _service.GetClienteByIdAsync(Id);

            if (result == null)
            {
                return NotFound(new
                {
                    message = $"No se encontró un cliente con Id: {Id}"
                });
            }

            return Ok(result);
        }

        [HttpGet("GetClienteByEmpresaId/{EmpresaId}")]
        public async Task<IActionResult> GetClienteByEmpresaId(int EmpresaId) 
        {
            var result = await _service.GetClienteByEmpresaIdAsync(EmpresaId);

            if (result == null) 
            {
                return NotFound(new 
                {
                    message = $"No se encontró un cliente con EmpresaId: {EmpresaId}"
                });
            }

            return Ok(result);
        }

        [HttpDelete("DeleteClieteAsync/{Id}")]
        public async Task<IActionResult> DeleteClienteAsync(int Id) 
        {
            var result = await _service.DeleteClienteAsync(Id);

            if (result == null)
            {
                return NotFound(new 
                {
                    message = "No se pudo eliminar el cliente con Id: " + Id
                });
            }

            return Ok(result);
        }

        [HttpPut("UpdateClienteAsync")]
        public async Task<IActionResult> UpdateClienteAsync(UpdateClienteModel model) 
        {
            var result = await _service.UpdateClienteAsync(model);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "No se pudo actualizar el cliente con Id: " + model.Id
                });
            }

            return Ok(result);
        }
    }
}
