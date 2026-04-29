using Microsoft.AspNetCore.Mvc;
using SGFE.Application.Interfaces.Roles;
using SGFE.Application.Models.Roles;

namespace SGFE.Api.Controllers.Roles
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly IRolService _servicer;

        public RolController(IRolService servicer)
        {
            _servicer = servicer;
        }

        [HttpPost("CreateRolAsync")]
        public async Task<IActionResult> CreateRolAsync([FromBody] CreateRolModel model)
        {
            var result = await _servicer.CreateRoleAsync(model);

            if (result == null) 
            {
                return NotFound(new 
                {
                    message = "No se pudo crear el rol"
                });
            }

            return Ok(result);
            
        }

        [HttpGet("GetAllRolesAsync")]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var result = await _servicer.GetAllRoleAsync();

            if (result == null)
            {
                return NotFound(new
                {
                    message = "No se encontraron datos en la base de datos"
                });
            }

            return Ok(result);
        }

        [HttpGet("GetRolByIdAsync/{roleId}")]
        public async Task<IActionResult> GetRoleByIdAsync(int roleId)
        {
            var result = await _servicer.GetRoleByIdAsync(roleId);

            if (result == null)
            {
                return NotFound(new
                {
                    message = $"No se encontró el rol con ID: {roleId}"
                });
            }

            return Ok(result);
        }

        [HttpDelete("DeleteRolAsync/{roleId}")]
        public async Task<IActionResult> DeleteRoleAsync(int roleId)
        {
            var result = await _servicer.DeleteRolAsync(roleId);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "No se encontró el rol con ID: {RoleId} ", roleId
                });
            }

            return Ok(result);
        }

        [HttpPut("UpdateRoleAsync")]
        public async Task<IActionResult> UpdateRoleAsync([FromBody] UpdateRolModel model) 
        {
            var result = await _servicer.UpdateRolAsync(model);

            if (result == null)
            {
                return NotFound(new
                {
                    message = $"No se pudo actualizar el rol con ID: {model.Id}"
                });
            }

            return Ok(result);
        }
    }
}
