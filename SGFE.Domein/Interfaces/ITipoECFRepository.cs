using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces
{
    public interface ITipoECFRepository
    {
        Task CrearTiposECFAsync(TipoECF entity);
        Task<List<TipoECF>> GetAllTiposECFAsync();
    }
}
