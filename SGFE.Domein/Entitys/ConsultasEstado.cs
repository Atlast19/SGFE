
namespace SGFE.Domein.Entitys;

public class ConsultasEstado
{
    public int Id { get; set; }

    public int FacturaId { get; set; }

    public string TrackId { get; set; }

    public string Estado { get; set; }

    public DateTime? FechaConsulta { get; set; }

    public string RespuestaDGII { get; set; }

    public virtual Factura Factura { get; set; }
}