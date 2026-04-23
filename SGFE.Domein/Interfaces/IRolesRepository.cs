
using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces
{
    public interface IRolesRepository
    {
        Task CreateRoleAsync(Roles entity);
        Task<Roles> GetRoleByIdAsync(int RoleId);
        Task<List<Roles>> GetAllRoleAsync(string SPname);
    }
}
