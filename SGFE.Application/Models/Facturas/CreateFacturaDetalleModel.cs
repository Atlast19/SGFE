
namespace SGFE.Application.Models.Facturas
{
    public class CreateFacturaDetalleModel
    {
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
