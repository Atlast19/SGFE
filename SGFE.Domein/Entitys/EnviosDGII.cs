
namespace SGFE.Domein.Entitys;

public partial class EnviosDGII
{
    public int Id { get; set; }

    public int FacturaId { get; set; }

    public string TrackId { get; set; }

    public string RequestXML { get; set; }

    public string ResponseXML { get; set; }

    public string EstadoEnvio { get; set; }

    public string CodigoRespuesta { get; set; }

    public string MensajeRespuesta { get; set; }

    public int? Intentos { get; set; }

    public DateTime? FechaEnvio { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Factura Factura { get; set; }
}