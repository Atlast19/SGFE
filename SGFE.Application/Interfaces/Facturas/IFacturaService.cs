
using SGFE.Domein.Entitys;

namespace SGFE.Application.Interfaces.Facturas
{
    public interface IFacturaService
    {
        Task CreateFacturaAsync(Factura factura, List<FacturaDetalle> detalles);
        Task<Factura> GetfacturaByIdAsync(int FacturaId);
        Task UpdateFacturaEstado(int facturaId, string estado);
        Task UpdateDGIIResponse(int facturaId, string trackId, string estado, string respuestaDGII, DateTime? fechaEnvio);
    }
}
