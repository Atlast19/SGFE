
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Interfaces;

namespace SGFE.Percistence.Repository.Configuracion
{
    public class ConfigurarionRepository : IConfiguracionRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ConfigurarionRepository> _logger;
        private readonly string _connectionString;

        public ConfigurarionRepository(IConfiguration configuration, ILogger<ConfigurarionRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public Task<List<Domein.Entitys.Configuracion>> GetConfiguracionAsync(int? empresaId, string clave)
        {
            throw new NotImplementedException();
        }

        public Task UpsetConfiguracionAync(int? empresaId, string clave, string valor, string descripcion)
        {
            throw new NotImplementedException();
        }
    }
}
