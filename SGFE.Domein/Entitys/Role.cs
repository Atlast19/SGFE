
namespace SGFE.Domein.Entitys;

public class Role
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Permisos { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}