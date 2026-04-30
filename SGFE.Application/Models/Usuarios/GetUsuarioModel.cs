
namespace SGFE.Application.Models.Usuarios
{
    public class GetUsuarioModel
    {
        public int Id { get; set; }

        public int RolId { get; set; }

        public int EmpresaId { get; set; }

        public string Nombre { get; set; }

        public string Email { get; set; }
    }
}
