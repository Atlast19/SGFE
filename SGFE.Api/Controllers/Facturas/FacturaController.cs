using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGFE.Application.Interfaces.Facturas;
using SGFE.Application.Models.Facturas;


namespace SGFE.Api.Controllers.Facturas
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaService _service;

        public FacturaController(IFacturaService service)
        {
            _service = service;
        }


        [Authorize(Roles = "Administrador")]
        [HttpPost("CreateFactura")]
        public async Task<IActionResult> CrearFactura([FromBody] CreateFacturaModel model)
        {
            try
            {
                // Validación básica
                if (model == null || model.Detalles == null || !model.Detalles.Any())
                    return BadRequest("La factura debe tener al menos un detalle.");

                var ncf = await _service.CreateFacturaAsync(model);

                return Ok(new
                {
                    mensaje = "Factura creada correctamente",
                    ncf = ncf
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al crear la factura",
                    detalle = ex.Message
                });
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("GetFacturaByIdAsync/{id}")]
        public async Task<IActionResult> GetFacturaByIdAsync(int id) 
        {
            var result = await _service.GetfacturaByIdAsync(id);
            if (result == null)
            {
                return NotFound(new
                {
                    message = $"No se encontró una factura con Id: {id}"
                });
            }
            return Ok(result);
        }

    }
}
