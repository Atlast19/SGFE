
using SGFE.Application.Models.Empresas;
using SGFE.Domein.Entitys;

namespace SGFE.Application.Interfaces.Empresas
{
    public interface IEmpresaService
    {
        Task<CreateEmpresaModel> CreateEmpresaAsync(CreateEmpresaModel entiry);
        Task<GetEmpresaModel> UpdateEmpresaAsync(UpdateEmpresaModel entity);
        Task<List<GetEmpresaModel>> GetAllEmpresaAsync();
        Task<GetEmpresaModel> GetEmpresaByIdAsync(int empresaId);
        Task<GetEmpresaModel> DeleteEmpresaAsync(int empresaId);
    }
}
