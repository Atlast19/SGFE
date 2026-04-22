
using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces
{
    public interface IUsuarioRepository
    {
        Task CreateUsuarioAsync(Usuario entity);
        Task GetUsuarioByEmailAsync(string email);
        Task GetUsuarioByIdAsync(int id);
        Task ActualizarUsuarioAsync (Usuario entity);
    }
}
