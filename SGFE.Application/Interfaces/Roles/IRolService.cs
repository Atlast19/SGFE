
using SGFE.Application.Models.Roles;
using SGFE.Domein.Entitys;

namespace SGFE.Application.Interfaces.Roles
{
    public interface IRolService
    {
        Task<GetRolModel> CreateRoleAsync(CreateRolModel model);
        Task<GetRolModel> GetRoleByIdAsync(int RolId);
        Task<List<GetRolModel>> GetAllRoleAsync();
        Task<GetRolModel> UpdateRolAsync(UpdateRolModel model);
        Task<GetRolModel> DeleteRolAsync(int RolId);
    }
}
