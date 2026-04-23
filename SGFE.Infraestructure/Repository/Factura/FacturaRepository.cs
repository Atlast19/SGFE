
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Interfaces;

namespace SGFE.Percistence.Repository.Factura
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<FacturaRepository> _logger;
        private readonly string _connectionString;  

        public FacturaRepository(IConfiguration configuration, ILogger<FacturaRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public Task CreateFacturaAsync(Domein.Entitys.Factura factura, List<Domein.Entitys.FacturaDetalle> detalles)
        {
            throw new NotImplementedException();
        }

        public Task<Domein.Entitys.Factura> GetfacturaByIdAsync(int FacturaId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDGIIResponse(int facturaId, string trackId, string estado, string respuestaDGII, DateTime? fechaEnvio)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFacturaEstado(int facturaId, string estado)
        {
            throw new NotImplementedException();
        }
    }
}
