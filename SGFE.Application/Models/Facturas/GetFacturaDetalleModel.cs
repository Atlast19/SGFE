

namespace SGFE.Application.Models.Facturas
{
    public class GetFacturaDetalleModel
    {
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Monto { get; set; }
        public decimal Itbis { get; set; }
    }
}
