using SGFE.Application.Interfaces.Roles;
using SGFE.Application.Models.Roles;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.Roles;


namespace SGFE.Application.Services.Roles
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _repository;

        public RolService(IRolRepository repository)
        {
            _repository = repository;
        }
        public async Task<GetRolModel> CreateRoleAsync(CreateRolModel model)
        {
           var RolEntity = new Rol
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion
            };

            var rol = await _repository.CreateRoleAsync(RolEntity);

            return new GetRolModel
            {
                Nombre = rol.Nombre,
                Descripcion = rol.Descripcion
            };
        }

        public async Task<GetRolModel> DeleteRolAsync(int RolId)
        {
            var RolById = await _repository.DeleteRolAsync(RolId);

            if (RolById == null)
                return null;

            return new GetRolModel
            {
                Id = RolById.Id,
                Nombre = RolById.Nombre,
                Descripcion = RolById.Descripcion
            };
        }

        public async Task<List<GetRolModel>> GetAllRoleAsync()
        {
            var roles = await _repository.GetAllRoleAsync();

            return roles.Select(rol => new GetRolModel
            {
                Id = rol.Id,
                Nombre = rol.Nombre,
                Descripcion = rol.Descripcion
            }).ToList();
        }

        public async Task<GetRolModel> GetRoleByIdAsync(int RolId)
        {
            var RolById =  await _repository.GetRoleByIdAsync(RolId);

            if(RolById == null)
                return null;

            return new GetRolModel
            {
                Id = RolById.Id,
                Nombre = RolById.Nombre,
                Descripcion = RolById.Descripcion
            };
        }

        public async Task<GetRolModel> UpdateRolAsync(UpdateRolModel model)
        {
            var RolEntity = new Rol
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion
            };

            var rol = await _repository.CreateRoleAsync(RolEntity);

            return new GetRolModel
            {
                Nombre = rol.Nombre,
                Descripcion = rol.Descripcion
            };
        }
    }
}
