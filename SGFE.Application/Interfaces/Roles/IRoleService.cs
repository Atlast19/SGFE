
using SGFE.Domein.Entitys;

namespace SGFE.Application.Interfaces.Roles
{
    public interface IRoleService
    {
        Task CreateRoleAsync(Rol entity);
        Task<Rol> GetRoleByIdAsync(int RoleId);
        Task<List<Rol>> GetAllRoleAsync();
    }
}
