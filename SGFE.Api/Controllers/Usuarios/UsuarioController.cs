using Microsoft.AspNetCore.Mvc;
using SGFE.Application.Interfaces.Usuarios;
using SGFE.Application.Models.Usuarios;

namespace SGFE.Api.Controllers.Usuarios
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpPost("CreateUsuarioAsync")]
        public async Task<IActionResult> CreateUsuarioAsync([FromBody] CreateUsuarioModel model)
        {
            var result = await _service.CreateUsuarioAsync(model);

            if (result == null) 
            {
                return NotFound(new 
                {
                    message = "No se puedo crear el usuario correctamente"
                });
            }

            return Ok(result);
        }

        [HttpGet("GetAllUsuarioAsync")]
        public async Task<IActionResult> GetAllUsuarioAsync()
        {
            var result = await _service.GetAllUsuarioAsync();

            if (result == null) 
            {
                return NotFound(new
                {
                    message = "No se encontraron datos en la base de datos"
                });
            }

            return Ok(result);
        }

        [HttpGet("GetUsuarioByIdAsync/{id}")]
        public async Task<IActionResult> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _service.GetUsuarioByIdAsync(id);

            if (usuario == null)
            {
                return NotFound(new 
                {
                    message = $"No se encontro ningun usuarios con el ID: {id}"
                });
            }

            return Ok(usuario);
        }

        [HttpGet("GetUsuarioByEmailAsync/{email}")]
        public async Task<IActionResult> GetUsuarioByEmailAsync(string email)
        {
            var usuario = await _service.GetUsuarioByEmailAsync(email);
            if (usuario == null)
            {
                return NotFound(new 
                {
                    message = $"No se encontro ningun usuarios con el Email: {email}"
                });
            }
            return Ok(usuario);
        }

        [HttpGet("GetRolesByUsuarioIdAsync/{usuarioId}")]
        public async Task<IActionResult> GetRolesByUsuarioIdAsync(int usuarioId)
        {
            var roles = await _service.GetRolesByUsuarioIdAsync(usuarioId);

            if (roles == null || roles.Count == 0)
            {
                return NotFound(new 
                {
                    message = $"No se encontro ningun usuario con el Rol ID: {usuarioId}"
                });
            }
            return Ok(roles);
        }

        [HttpGet("GetUsuarioByEmailForLogin/{email}")]
        public async Task<IActionResult> GetUsuarioByEmailForLogin(string email)
        {
            var usuario = await _service.GetEmailForLogin(email);
            if (usuario == null)
            {
                return NotFound(new 
                {
                    message = $"No se encontro el Email: {email}"
                });
            }
            return Ok(usuario);
        }

        [HttpPut("UpdateUsuarioAsync")]
        public async Task<IActionResult> UpdateUsuarioAsync([FromBody] UpdateUsuarioModel model)
        {
            var result = await _service.UpdateUsuarioAsync(model);

            if (result == null) 
            {
                return NotFound(new
                {
                    message = $"No se pudo actualizar el usuario con ID {model.Id}"
                });
            }

            return Ok(result);
        }
    }
}
