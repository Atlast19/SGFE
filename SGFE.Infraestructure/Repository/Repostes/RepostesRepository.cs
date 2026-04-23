using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Entitys.ReportesEntirys;
using SGFE.Domein.Interfaces;

namespace SGFE.Percistence.Repository.Repostes
{
    public class RepostesRepository : IResportesRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RepostesRepository> _logger;
        private readonly string _connectionString;

        public RepostesRepository(IConfiguration configuration, ILogger<RepostesRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public Task<List<FacturaRepostes>> GetFacturaRepostesAsync(int empresaId, DateTime? fechaDesde, DateTime? fechaHasta, string estado)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResumenFacturas>> GetResumenFacturasAsync(int empresaId, int? anio, int? mes)
        {
            throw new NotImplementedException();
        }
    }
}
