
using SGFE.Domein.Entitys;

namespace SGFE.Application.Interfaces.Usuarios
{
    public interface IUsuarioService
    {
        Task CreateUsuarioAsync(Usuario entity);
        Task UpdateUsuarioAsync(Usuario entity);
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<Usuario> GetUsuarioByEmailAsync(string email);
    }
}
