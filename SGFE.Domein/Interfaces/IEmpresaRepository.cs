
using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces
{
    public interface IEmpresaRepository
    {
        Task CreateEmpresaAsync(Empresa entiry);
        Task<List<Empresa>> GetAllEmpresaAsync(string SPname);
        Task GetEmpresaByIdAsync (int empresaId);
        Task UpdateEmpresaAsync(Empresa entity);
    }
}
