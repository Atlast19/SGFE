
using SGFE.Domein.Entitys;

namespace SGFE.Application.Interfaces.Empresas
{
    public interface IEmpresaService
    {
        Task CreateEmpresaAsync(Empresa entiry);
        Task UpdateEmpresaAsync(Empresa entity);
        Task<List<Empresa>> GetAllEmpresaAsync();
        Task<Empresa> GetEmpresaByIdAsync(int empresaId);
    }
}
