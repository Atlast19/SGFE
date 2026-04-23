
using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces
{
    public interface ISecuenciasNCFRepository
    {
        Task<string> GetNextSecuenciaNCFAsync(string SPname);
        Task RegistrarSecuenciaAsync(SecuenciaNCF entity);
    }
}
