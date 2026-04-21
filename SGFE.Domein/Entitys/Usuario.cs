
namespace SGFE.Domein.Entitys;

public class Usuario
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string PasswordHash { get; set; }

    public string ApiKey { get; set; }

    public int RolId { get; set; }

    public bool? Activo { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Role Rol { get; set; }
}