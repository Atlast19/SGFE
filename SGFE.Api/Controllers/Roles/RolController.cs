using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SGFE.Application.Interfaces.Roles;
using SGFE.Application.Models.Roles;
using SGFE.Application.Services.Roles;

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

        [Authorize(Roles = "Administrador")]
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


        [Authorize(Roles = "Administrador")]
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


        [Authorize(Roles = "Administrador")]
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


        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeleteRolAsync/{roleId}")]
        public async Task<IActionResult> DeleteRoleAsync(int roleId)
        {
            var result = await _servicer.DeleteRolAsync(roleId);
            if (result == null)
            {
                return NotFound(new
                {
                    message = $"No se encontró el rol con ID: {roleId}" 
                });
            }
            return Ok(result);
        }


        [Authorize(Roles = "Administrador")]
        [HttpPut("UpdateRoleAsync")]
        public async Task<IActionResult> UpdateRoleAsync([FromBody] UpdateRolModel model) 
        {
            var result = await _servicer.UpdateRolAsync(model);
            
            if (result == null)
            {
                return NotFound(new
                {
                    message = $"No se pudo actualizar el rol"
                });
            }
            return Ok(result);
            
        }
    }
}
