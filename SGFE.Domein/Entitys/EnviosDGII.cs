
namespace SGFE.Domein.Entitys;

public class EnviosDGII
{
    public int Id { get; set; }

    public int FacturaId { get; set; }

    public string TrackId { get; set; }

    public DateTime? FechaEnvio { get; set; }

    public string Respuesta { get; set; }

    public string Estado { get; set; }

    public virtual Factura Factura { get; set; }
}