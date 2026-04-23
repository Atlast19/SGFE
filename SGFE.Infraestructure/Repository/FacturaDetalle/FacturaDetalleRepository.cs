
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Interfaces;

namespace SGFE.Percistence.Repository.FacturaDetalle
{
    public class FacturaDetalleRepository : IFacturaDetalleRepository
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<FacturaDetalleRepository> logger;

        public FacturaDetalleRepository(IConfiguration configuration, ILogger<FacturaDetalleRepository> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }
        public Task CreateFacturaDetalleAsync(Domein.Entitys.FacturaDetalle entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFacturaDetalleAsync(int IdFacturaDetalle)
        {
            throw new NotImplementedException();
        }

        public Task<List<Domein.Entitys.FacturaDetalle>> GetFacturaDetalleByFactura(int IdFactura)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFacturaDetalleAsync(Domein.Entitys.FacturaDetalle entity)
        {
            throw new NotImplementedException();
        }
    }
}
