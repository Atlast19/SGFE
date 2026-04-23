using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Interfaces;

namespace SGFE.Percistence.Repository.EnviosDGII
{
    public class EnviosDGIIRepository : IEnviosDGIIRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EnviosDGIIRepository> _logger;
        private readonly string _connectionString;

        public EnviosDGIIRepository(IConfiguration configuration, ILogger<EnviosDGIIRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public Task RegistrarEnvioDGIIAsync(int facturaId, string trackId, string requestXml, string estadoEnvio)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEnvioDGIIAsync(string trackId, string responseXml, string estadoEnvio, string codigoRespuesta, string mensajeRespuesta)
        {
            throw new NotImplementedException();
        }
    }
}
