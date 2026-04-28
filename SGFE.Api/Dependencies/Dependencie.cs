using SGFE.Application.Interfaces.Roles;
using SGFE.Application.Interfaces.Usuarios;
using SGFE.Application.Services.Roles;
using SGFE.Application.Services.Ususarios;
using SGFE.Domein.Interfaces.Roles;
using SGFE.Domein.Interfaces.Usuarios;
using SGFE.Percistence.Repository.Roles;
using SGFE.Percistence.Repository.Usuarios;

namespace SGFE.Api.Dependencies
{
    public static class Dependencie
    {
        public static void RegisterOfDependencies(this IServiceCollection service)
        {
            #region Usuarios
            service.AddScoped<IUsuarioRepository, UsuarioRepository>();
            service.AddScoped<IUsuarioService, UsuarioService>();
            #endregion

            #region Roles
            service.AddScoped<IRolRepository, RolRepository>();
            service.AddScoped<IRolService, RolService>();
            #endregion
        }
    }
}
