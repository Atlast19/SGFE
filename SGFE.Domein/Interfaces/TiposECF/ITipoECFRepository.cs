using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces.TiposECF
{
    public interface ITipoECFRepository
    {
        Task<TipoECF> CrearTiposECFAsync(TipoECF entity);
        Task<List<TipoECF>> GetAllTiposECFAsync();
        Task<TipoECF> GetTipoECFByIdAsync(int id);
        Task<TipoECF> UpdateTipoECFAsync(TipoECF entity);
        Task<TipoECF> DeleteTipoECFAsync(int id);
    }
}
