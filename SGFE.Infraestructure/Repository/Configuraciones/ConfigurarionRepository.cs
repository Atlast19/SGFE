using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.Configuraciones;

namespace SGFE.Percistence.Repository.Configuraciones
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
        public Task<List<Configuracion>> GetConfiguracionAsync(int? empresaId, string clave)
        {
            throw new NotImplementedException();
        }

        public Task UpsetConfiguracionAync(int? empresaId, string clave, string valor, string descripcion)
        {
            throw new NotImplementedException();
        }
    }
}
