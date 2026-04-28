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

        //[HttpPost("CreateUsuarioAsync")]
        //public async Task<IActionResult> CreateUsuarioAsync([FromBody] UsuarioModel model) 
        //{
        //    await _service.CreateUsuarioAsync(model);
        //    return Ok();
        //}
    }
}
