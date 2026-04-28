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
            try
            {
                var roles = await _servicer.CreateRoleAsync(model);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllRolesAsync")]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            try
            {
                var roles = await _servicer.GetAllRoleAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetRolByIdAsync/{roleId}")]
        public async Task<IActionResult> GetRoleByIdAsync(int roleId)
        {
            try
            {
                var role = await _servicer.GetRoleByIdAsync(roleId);
                if (role == null)
                    return NotFound();
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteRolAsync/{roleId}")]
        public async Task<IActionResult> DeleteRoleAsync(int roleId)
        {
            try
            {
                var role = await _servicer.DeleteRolAsync(roleId);
                if (role == null)
                    return NotFound();
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateRoleAsync")]
        public async Task<IActionResult> UpdateRoleAsync([FromBody] UpdateRolModel model) 
        {
            try
            {
                await _servicer.UpdateRolAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
