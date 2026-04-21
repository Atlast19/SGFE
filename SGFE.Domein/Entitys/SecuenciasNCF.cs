
namespace SGFE.Domein.Entitys;

public class SecuenciasNCF
{
    public int Id { get; set; }

    public int EmpresaId { get; set; }

    public string TipoECF { get; set; }

    public long? SecuenciaActual { get; set; }

    public long? Limite { get; set; }

    public virtual Empresa Empresa { get; set; }
}