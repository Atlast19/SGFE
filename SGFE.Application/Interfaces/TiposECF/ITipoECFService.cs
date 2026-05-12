using SGFE.Application.Models.TiposECF;

namespace SGFE.Application.Interfaces.TiposECF
{
    public interface ITipoECFService
    {
        Task<CreateTipoECFModel> CrearTiposECFAsync(CreateTipoECFModel model);
        Task<List<GetTipoECFModel>> GetAllTiposECFAsync();
        Task<GetTipoECFModel> DeleteTipoECFAsync(int id);
        Task<GetTipoECFModel> GetTipoECFByIdAsync(int id);
        Task<UpdateTipoECFModel> UpdateTipoECFAsync(UpdateTipoECFModel model);
    }
}
