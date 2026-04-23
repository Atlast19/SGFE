
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Interfaces;

namespace SGFE.Percistence.Repository.TiposECF
{
    public class TiposECFRepository : ITiposECFRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TiposECFRepository> _logger;
        private readonly string _connectionString;

        public TiposECFRepository(IConfiguration configuration, ILogger<TiposECFRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public Task CrearTiposECFAsync(Domein.Entitys.TiposECF entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Domein.Entitys.TiposECF>> GetAllTiposECFAsync(string SPname)
        {
            throw new NotImplementedException();
        }
    }
}
