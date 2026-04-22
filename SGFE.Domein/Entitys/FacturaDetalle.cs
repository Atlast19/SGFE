
namespace SGFE.Domein.Entitys;

public partial class FacturaDetalle
{
    public int Id { get; set; }

    public int FacturaId { get; set; }

    public string Descripcion { get; set; }

    public decimal Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Monto { get; set; }

    public decimal Itbis { get; set; }

    public decimal? Descuento { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Factura Factura { get; set; }
}