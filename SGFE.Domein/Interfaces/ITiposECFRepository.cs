using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces
{
    public interface ITiposECFRepository
    {
        Task CrearTiposECFAsync(TiposECF entity);
        Task<List<TiposECF>> GetAllTiposECFAsync(string SPname);
    }
}
