
using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces
{
    public interface IEmpresaRepository
    {
        Task CreateEmpresaAsync(Empresa entiry);
        Task UpdateEmpresaAsync(Empresa entity);
        Task<List<Empresa>> GetAllEmpresaAsync(string SPname);
        Task<Empresa> GetEmpresaByIdAsync (int empresaId);
    }
}
