
using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces
{
    public interface IFacturaDetalleRepository
    {
        Task CreateFacturaDetalleAsync(FacturaDetalle entity);
        Task UpdateFacturaDetalleAsync (FacturaDetalle entity);
        Task DeleteFacturaDetalleAsync(int IdFacturaDetalle);
        Task<List<FacturaDetalle>> GetFacturaDetalleByFactura(int IdFactura);
    }

}
