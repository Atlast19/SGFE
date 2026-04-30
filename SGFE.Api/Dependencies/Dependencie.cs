using SGFE.Application.Interfaces.Empresas;
using SGFE.Application.Interfaces.PasswordHasher;
using SGFE.Application.Interfaces.Roles;
using SGFE.Application.Interfaces.Usuarios;
using SGFE.Application.Services.AuthService;
using SGFE.Application.Services.AuthServices;
using SGFE.Application.Services.Empresas;
using SGFE.Application.Services.PasswordHasher;
using SGFE.Application.Services.Roles;
using SGFE.Application.Services.Ususarios;
using SGFE.Domein.Interfaces.Empresas;
using SGFE.Domein.Interfaces.Roles;
using SGFE.Domein.Interfaces.Usuarios;
using SGFE.Percistence.Repository.Empresas;
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
            service.AddScoped<AuthService>();
            service.AddScoped<JwtService>();

            service.AddScoped<IPasswordHash, PasswordHashService>();
            #endregion

            #region Roles
            service.AddScoped<IRolRepository, RolRepository>();
            service.AddScoped<IRolService, RolService>();
            #endregion

            #region Empresas
            service.AddScoped<IEmpresaService, EmpresaService>();
            service.AddScoped<IEmpresaRepository, EmpresaRepository>();
            #endregion
        }
    }
}
