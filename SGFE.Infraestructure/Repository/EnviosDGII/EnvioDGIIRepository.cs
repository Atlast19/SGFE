using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Interfaces.EnviosDGII;

namespace SGFE.Percistence.Repository.EnviosDGII
{
    public class EnvioDGIIRepository : IEnvioDGIIRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EnvioDGIIRepository> _logger;
        private readonly string _connectionString;

        public EnvioDGIIRepository(IConfiguration configuration, ILogger<EnvioDGIIRepository> logger)
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
