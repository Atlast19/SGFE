
namespace SGFE.Domein.Entitys;

public partial class Usuario
{
    public int Id { get; set; }

    public int RolId { get; set; }

    public int EmpresaId { get; set; }

    public string Nombre { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string ApiKey { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual Empresa Empresa { get; set; }

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual Rol Rol { get; set; }
}