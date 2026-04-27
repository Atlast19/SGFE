using SGFE.Application.Interfaces.Facturas;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.Facturas;

namespace SGFE.Application.Services.Facturas
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _repository;

        public FacturaService(IFacturaRepository repository)
        {
            _repository = repository;
        }
        public async Task CreateFacturaAsync(Factura factura, List<FacturaDetalle> detalles)
        {
            await _repository.CreateFacturaAsync(factura, detalles);
        }

        public async Task<Factura> GetfacturaByIdAsync(int FacturaId)
        {
            return await _repository.GetfacturaByIdAsync(FacturaId);
        }

        public async Task UpdateDGIIResponse(int facturaId, string trackId, string estado, string respuestaDGII, DateTime? fechaEnvio)
        {
            await _repository.UpdateDGIIResponse(facturaId, trackId, estado, respuestaDGII, fechaEnvio);
        }

        public async Task UpdateFacturaEstado(int facturaId, string estado)
        {
            await _repository.UpdateFacturaEstado(facturaId, estado);
        }
    }
}
