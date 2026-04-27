using Microsoft.AspNetCore.Mvc;
using SGFE.Application.Interfaces.Usuarios;

namespace SGFE.Api.Controllers.Usuarios
{
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuaroService _service;

        public UsuarioController(IUsuaroService service)
        {
            _service = service;
        }
    }
}
