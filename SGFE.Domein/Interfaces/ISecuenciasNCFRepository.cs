
using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces
{
    public interface ISecuenciasNCFRepository
    {
        Task<string> GetNextSecuenciaNCFAsync();
        Task RegistrarSecuenciaAsync(SecuenciaNCF entity);
    }
}
