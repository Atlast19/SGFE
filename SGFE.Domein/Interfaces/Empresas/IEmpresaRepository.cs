using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces.Empresas
{
    public interface IEmpresaRepository
    {
        Task<Empresa> CreateEmpresaAsync(Empresa entiry);
        Task<Empresa> UpdateEmpresaAsync(Empresa entity);
        Task<List<Empresa>> GetAllEmpresaAsync();
        Task<Empresa> GetEmpresaByIdAsync (int empresaId);

        Task<Empresa> DeleteEmpresaAsync(int empresaId);
    }
}
