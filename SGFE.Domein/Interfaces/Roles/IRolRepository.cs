using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces.Roles
{
    public interface IRolRepository
    {
        Task<Rol> CreateRoleAsync(Rol entity);
        Task<Rol> GetRoleByIdAsync(int RoleId);
        Task<List<Rol>> GetAllRoleAsync();
        Task<Rol> UpdateRolAsync (Rol entity);
        Task<Rol> DeleteRolAsync(int RoleId);
    }
}
