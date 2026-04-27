namespace SGFE.Domein.Interfaces.EnviosDGII
{
    public interface IEnvioDGIIRepository
    {
        Task RegistrarEnvioDGIIAsync(int facturaId, string trackId, string requestXml, string estadoEnvio);

        Task UpdateEnvioDGIIAsync(string trackId, string responseXml, string estadoEnvio, string codigoRespuesta, string mensajeRespuesta);
    }
}
