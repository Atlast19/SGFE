using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces.Empresas
{
    public interface IEmpresaRepository
    {
        Task CreateEmpresaAsync(Empresa entiry);
        Task UpdateEmpresaAsync(Empresa entity);
        Task<List<Empresa>> GetAllEmpresaAsync();
        Task<Empresa> GetEmpresaByIdAsync (int empresaId);
    }
}
