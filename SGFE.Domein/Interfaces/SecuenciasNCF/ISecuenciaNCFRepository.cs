using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces.SecuenciasNCF
{
    public interface ISecuenciaNCFRepository
    {
        Task<string> GetNextSecuenciaNCFAsync();
        Task RegistrarSecuenciaAsync(SecuenciaNCF entity);
    }
}
