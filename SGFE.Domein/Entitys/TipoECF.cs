
namespace SGFE.Domein.Entitys;

public partial class TipoECF
{
    public int Id { get; set; }

    public string Codigo { get; set; }

    public string Descripcion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<SecuenciaNCF> SecuenciaNCFs { get; set; } = new List<SecuenciaNCF>();
}