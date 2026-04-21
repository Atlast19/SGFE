
namespace SGFE.Domein.Entitys;

public class Cliente
{
    public int Id { get; set; }

    public int EmpresaId { get; set; }

    public string Nombre { get; set; }

    public string RncOCedula { get; set; }

    public string TipoDocumento { get; set; }

    public string Email { get; set; }

    public string Telefono { get; set; }

    public string Direccion { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Empresa Empresa { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}