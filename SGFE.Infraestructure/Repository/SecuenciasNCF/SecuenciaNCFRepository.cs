
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces;

namespace SGFE.Percistence.Repository.SecuenciasNCF
{
    public class SecuenciaNCFRepository : ISecuenciasNCFRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SecuenciaNCFRepository> _logger;
        private readonly string _connectionString;

        public SecuenciaNCFRepository(IConfiguration configuration, ILogger<SecuenciaNCFRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public Task<string> GetNextSecuenciaNCFAsync()
        {
            throw new NotImplementedException();
        }

        public Task RegistrarSecuenciaAsync(SecuenciaNCF entity)
        {
            throw new NotImplementedException();
        }
    }
}
