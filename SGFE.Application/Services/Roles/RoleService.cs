using SGFE.Application.Interfaces.Roles;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces;


namespace SGFE.Application.Services.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IRolRepository _repository;

        public RoleService(IRolRepository repository)
        {
            _repository = repository;
        }
        public async Task CreateRoleAsync(Rol entity)
        {
            await _repository.CreateRoleAsync(entity);
        }

        public async Task<List<Rol>> GetAllRoleAsync()
        {
            return await _repository.GetAllRoleAsync();
        }

        public async Task<Rol> GetRoleByIdAsync(int RoleId)
        {
            return await _repository.GetRoleByIdAsync(RoleId);
        }
    }
}
