using SGFE.Application.Models.TiposECF;

namespace SGFE.Application.Interfaces.TiposECF
{
    public interface ITipoECFService
    {
        Task<CreateTipoECF> CrearTiposECFAsync(CreateTipoECF model);
        Task<List<GetTipoECF>> GetAllTiposECFAsync();
        Task<GetTipoECF> DeleteTipoECFAsync(int id);
        Task<GetTipoECF> GetTipoECFByIdAsync(int id);
        Task<UpdateTipoECF> UpdateTipoECFAsync(UpdateTipoECF model);
    }
}
