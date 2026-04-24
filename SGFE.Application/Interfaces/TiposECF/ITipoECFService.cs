
using SGFE.Domein.Entitys;

namespace SGFE.Application.Interfaces.TiposECF
{
    public interface ITipoECFService
    {
        Task CrearTiposECFAsync(TipoECF entity);
        Task<List<TipoECF>> GetAllTiposECFAsync();
    }
}
