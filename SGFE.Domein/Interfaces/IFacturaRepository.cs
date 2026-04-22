
using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces
{
    public interface IFacturaRepository
    {
        Task CreateFacturaAsync(Factura entiry);
        Task GetfacturaByIdAsync(int FacturaId);
        Task UpdateFacturaEstado(string SPname);
        Task UpdateDGIIResponse(string SPname);
    }
}
