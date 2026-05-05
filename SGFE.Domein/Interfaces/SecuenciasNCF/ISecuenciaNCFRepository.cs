using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces.SecuenciasNCF
{
    public interface ISecuenciaNCFRepository
    {
        Task<string> GetNextSecuenciaNCFAsync(int empresaId, int tipoECFId);
        Task RegistrarSecuenciaAsync(SecuenciaNCF entity);
    }
}
