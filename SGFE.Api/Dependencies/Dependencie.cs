using SGFE.Application.Interfaces.CertificadosDigitales;
using SGFE.Application.Interfaces.Clientes;
using SGFE.Application.Interfaces.Empresas;
using SGFE.Application.Interfaces.PasswordHasher;
using SGFE.Application.Interfaces.Reportes;
using SGFE.Application.Interfaces.Roles;
using SGFE.Application.Interfaces.SecuenciasNCF;
using SGFE.Application.Interfaces.TiposECF;
using SGFE.Application.Interfaces.Usuarios;
using SGFE.Application.Services.AuthService;
using SGFE.Application.Services.AuthServices;
using SGFE.Application.Services.CertificadosDigitales;
using SGFE.Application.Services.Clientes;
using SGFE.Application.Services.Empresas;
using SGFE.Application.Services.PasswordHasher;
using SGFE.Application.Services.Reportes;
using SGFE.Application.Services.Roles;
using SGFE.Application.Services.SecuenciasNCF;
using SGFE.Application.Services.TiposECF;
using SGFE.Application.Services.Ususarios;
using SGFE.Domein.Interfaces.CertificadosDigitales;
using SGFE.Domein.Interfaces.Clientes;
using SGFE.Domein.Interfaces.Empresas;
using SGFE.Domein.Interfaces.Reportes;
using SGFE.Domein.Interfaces.Roles;
using SGFE.Domein.Interfaces.SecuenciasNCF;
using SGFE.Domein.Interfaces.TiposECF;
using SGFE.Domein.Interfaces.Usuarios;
using SGFE.Percistence.Repository.CertificadosDigitales;
using SGFE.Percistence.Repository.Clientes;
using SGFE.Percistence.Repository.Empresas;
using SGFE.Percistence.Repository.Repostes;
using SGFE.Percistence.Repository.Roles;
using SGFE.Percistence.Repository.SecuenciasNCF;
using SGFE.Percistence.Repository.TiposECF;
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

            #region Clientes
            service.AddScoped<IClienteService, ClienteService>();
            service.AddScoped<IClienteRepository, ClienteRepository>();
            #endregion

            #region Tipo(e-CF)
            service.AddScoped<ITipoECFService, TipoECFService>();
            service.AddScoped<ITipoECFRepository, TiposECFRepository>();
            #endregion

            #region Reportes
            service.AddScoped<IReporteService, ReporteService>();
            service.AddScoped<IReporteRepository, ReposteRepository>();
            #endregion

            #region Certificado Digital
            service.AddScoped<ICertificadoDigitalService, CertificadoDigicalService>();
            service.AddScoped<ICertificadosDigitalRepository, CertificadosDigitalRepository>();
            #endregion

            #region SecuenciaNCF
            service.AddScoped<ISecuenciaNCFService, SecuenciaNCFService>();
            service.AddScoped<ISecuenciaNCFRepository, SecuenciaNCFRepository>();
            #endregion
        }
    }
}
