
namespace SGFE.Domein.Entitys;

public class CertificadosDigitale
{
    public int Id { get; set; }

    public int EmpresaId { get; set; }

    public byte[] Archivo { get; set; }

    public string Password { get; set; }

    public DateTime? FechaExpiracion { get; set; }

    public bool? Activo { get; set; }

    public virtual Empresa Empresa { get; set; }
}