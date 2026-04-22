
using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces
{
    public interface IEnviosDGIIRepository
    {
        Task RegistrarEnvioDGIIAsync(EnviosDGII entiry);

        Task UpdateRespuestaEnvioDGIIAsync(EnviosDGII entity);
    }
}
