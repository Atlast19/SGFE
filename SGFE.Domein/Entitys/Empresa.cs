
namespace SGFE.Domein.Entitys;

public partial class Empresa
{
    public int Id { get; set; }

    public string RNC { get; set; }

    public string Nombre { get; set; }

    public string NombreComercial { get; set; }

    public string Direccion { get; set; }

    public string Telefono { get; set; }

    public string Email { get; set; }

    public string Ambiente { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<CertificadosDigital> CertificadosDigitales { get; set; } = new List<CertificadosDigital>();

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Configuracion> Configuracions { get; set; } = new List<Configuracion>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual ICollection<SecuenciaNCF> SecuenciaNCFs { get; set; } = new List<SecuenciaNCF>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}