using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces.Usuarios
{
    public interface IUsuarioRepository
    {
        Task CreateUsuarioAsync(Usuario entity);
        Task UpdateUsuarioAsync (Usuario entity);
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<Usuario> GetUsuarioByEmailAsync(string email);
        Task<Usuario> LoginAsync(string Email, string PasswordHash);
    }
}
