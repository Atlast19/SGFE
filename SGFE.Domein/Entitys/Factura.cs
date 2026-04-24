
namespace SGFE.Domein.Entitys;

public partial class Factura
{
    public int Id { get; set; }

    public int EmpresaId { get; set; }

    public int ClienteId { get; set; }

    public int TipoECFId { get; set; }

    public string NCF { get; set; }

    public DateTime FechaEmision { get; set; }

    public DateTime? FechaFirma { get; set; }

    public decimal MontoTotal { get; set; }

    public decimal ItbisTotal { get; set; }

    public decimal? OtrosImpuestos { get; set; }

    public string Estado { get; set; }

    public string XmlGenerado { get; set; }

    public string TrackId { get; set; }

    public string RespuestaDGII { get; set; }

    public DateTime? FechaEnvio { get; set; }

    public DateTime? FechaConsulta { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual Cliente Cliente { get; set; }

    public virtual Empresa Empresa { get; set; }

    public virtual ICollection<EnvioDGII> EnviosDGIIs { get; set; } = new List<EnvioDGII>();

    public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();

    public virtual TipoECF TipoECF { get; set; }
}