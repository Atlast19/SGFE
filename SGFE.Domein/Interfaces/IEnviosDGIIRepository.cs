
namespace SGFE.Domein.Interfaces
{
    public interface IEnviosDGIIRepository
    {
        Task RegistrarEnvioDGIIAsync(int facturaId, string trackId, string requestXml, string estadoEnvio);

        Task UpdateEnvioDGIIAsync(string trackId, string responseXml, string estadoEnvio, string codigoRespuesta, string mensajeRespuesta);
    }
}
