
namespace SGFE.Domein.Entitys;

public class FacturaDetalle
{
    public int Id { get; set; }

    public int FacturaId { get; set; }

    public decimal Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal TasaITBIS { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? ITBIS { get; set; }

    public decimal? Total { get; set; }

    public virtual Factura Factura { get; set; }
}