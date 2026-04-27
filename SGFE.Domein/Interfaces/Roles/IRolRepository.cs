using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces.Roles
{
    public interface IRolRepository
    {
        Task CreateRoleAsync(Rol entity);
        Task<Rol> GetRoleByIdAsync(int RoleId);
        Task<List<Rol>> GetAllRoleAsync();
    }
}
