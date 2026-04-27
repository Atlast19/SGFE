using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces.TiposECF
{
    public interface ITipoECFRepository
    {
        Task CrearTiposECFAsync(TipoECF entity);
        Task<List<TipoECF>> GetAllTiposECFAsync();
    }
}
