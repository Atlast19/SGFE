using Microsoft.AspNetCore.Mvc;
using SGFE.Application.Interfaces.Roles;

namespace SGFE.Api.Controllers.Roles
{
    public class RolController : ControllerBase
    {
        private readonly IRoleService _servicer;

        public RolController(IRoleService servicer)
        {
            _servicer = servicer;
        }


    }
}
