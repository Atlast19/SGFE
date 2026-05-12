
using SGFE.Application.Models.Facturas;
using SGFE.Domein.Entitys;

namespace SGFE.Application.Interfaces.Facturas
{
    public interface IFacturaService
    {
        Task<string> CreateFacturaAsync(CreateFacturaModel model);
        Task<GetFacturaModel> GetfacturaByIdAsync(int FacturaId);
        Task UpdateFacturaEstado(int facturaId, string estado);
        Task UpdateDGIIResponse(int facturaId, string trackId, string estado, string respuestaDGII, DateTime? fechaEnvio);
    }
}
