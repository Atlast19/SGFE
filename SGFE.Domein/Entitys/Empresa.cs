
namespace SGFE.Domein.Entitys;

public class Empresa
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string RNC { get; set; }

    public string Direccion { get; set; }

    public string Telefono { get; set; }

    public string Email { get; set; }

    public string Ambiente { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<CertificadosDigitale> CertificadosDigitales { get; set; } = new List<CertificadosDigitale>();

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<SecuenciasNCF> SecuenciasNCFs { get; set; } = new List<SecuenciasNCF>();
}