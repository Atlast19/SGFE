using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces.Facturas
{
    public interface IFacturaRepository
    {
        Task<(int facturaId, string ncf)> CreateFacturaAsync(Factura factura, List<FacturaDetalle> detalles);
        Task GuardarXmlAsync(int facturaId, string xml);
        Task<Factura> GetfacturaByIdAsync(int FacturaId);
        Task UpdateFacturaEstado(int facturaId, string estado);
        Task UpdateDGIIResponse(int facturaId, string trackId, string estado, string respuestaDGII, DateTime? fechaEnvio);
    }
}
