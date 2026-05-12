using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces.SecuenciasNCF
{
    public interface ISecuenciaNCFRepository
    {
        Task RegistrarSecuenciaAsync(SecuenciaNCF entity);
    }
}
