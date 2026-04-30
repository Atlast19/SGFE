using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces.Usuarios
{
    public interface IUsuarioRepository
    {
        Task<Usuario> CreateUsuarioAsync(Usuario entity);
        Task<Usuario> UpdateUsuarioAsync (Usuario entity);
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<Usuario> GetUsuarioByEmailAsync(string email);
        Task<List<Usuario>> GetAllUsuariosAsync();
        Task<List<string>> GetRolesByUsuarioIdAsync(int usuarioId);
        Task<Usuario> GetEmailForLogin(string email);
        Task<Usuario> DeleteUsuarioAsync(int Id);
    }
}
