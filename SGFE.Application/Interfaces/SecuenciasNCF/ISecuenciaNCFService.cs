
using SGFE.Application.Models.SecuenciaNCF;

namespace SGFE.Application.Interfaces.SecuenciasNCF
{
    public interface ISecuenciaNCFService
    {
        //Task<GetNCFResponseModel> GetNextSecuenciaNCFAsync(CreateSecuenciaNCFModel request);

        Task RegistrarAsync(RegistrarSecuenciaNCFModel model);
    }
}
