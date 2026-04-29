
namespace SGFE.Application.Models.Usuarios
{
    public class CreateUsuarioModel
    {
        public int EmpresaId { get; set; }

        public string Nombre { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
    }
}
