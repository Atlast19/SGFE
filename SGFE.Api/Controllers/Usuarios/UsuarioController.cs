using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGFE.Application.Interfaces.Usuarios;
using SGFE.Application.Models.Usuarios;
using SGFE.Application.Services.AuthServices;
using System.Runtime.InteropServices;

namespace SGFE.Api.Controllers.Usuarios
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;
        private readonly AuthService _auth;

        public UsuarioController(IUsuarioService service, AuthService auth)
        {
            _service = service;
            _auth = auth;
        }

        [AllowAnonymous]
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


        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        [HttpDelete("DeleteUsuarioAsync/{Id}")]
        public async Task<IActionResult> DeleteUsuarioAsync(int Id) 
        {
            var result = await _service.DeleteUsuarioAsync(Id);

            if (result == null) 
            {
                return NotFound(new 
                {
                    message = $"No se pudo eliminar el usuadio del ID: {Id}"
                });
            }
            return Ok(result);
        }
        
        [Authorize]
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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _auth.Login(request.Email, request.Password);

            return Ok(new { token });
        }

    }
}
