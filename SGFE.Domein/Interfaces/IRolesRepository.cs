
using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces
{
    public interface IRolesRepository
    {
        Task CreateRoleAsync(Roles entity);
        Task<List<Roles>> GetAllRoleAsync(string SPname);
        Task GetRoleByIdAsync(int RoleId);
    }
}
