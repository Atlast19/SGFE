
namespace SGFE.Domein.Entitys;

public class Factura
{
    public int Id { get; set; }

    public int EmpresaId { get; set; }

    public int ClienteId { get; set; }

    public string TipoECF { get; set; }

    public string NumeroECF { get; set; }

    public DateTime FechaEmision { get; set; }

    public decimal MontoTotal { get; set; }

    public decimal MontoImpuestos { get; set; }

    public decimal MontoGravado { get; set; }

    public string Estado { get; set; }

    public string TrackId { get; set; }

    public string XmlGenerado { get; set; }

    public string XmlFirmado { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Cliente Cliente { get; set; }

    public virtual ICollection<ConsultasEstado> ConsultasEstados { get; set; } = new List<ConsultasEstado>();

    public virtual Empresa Empresa { get; set; }

    public virtual ICollection<EnviosDGII> EnviosDGIIs { get; set; } = new List<EnviosDGII>();

    public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();
}