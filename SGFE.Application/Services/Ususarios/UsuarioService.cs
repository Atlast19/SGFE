using SGFE.Application.Interfaces.PasswordHasher;
using SGFE.Application.Interfaces.Usuarios;
using SGFE.Application.Models.Usuarios;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.Usuarios;

namespace SGFE.Application.Services.Ususarios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IPasswordHash _hasher;

        public UsuarioService(IUsuarioRepository repository, IPasswordHash hasher)
        {
            _repository = repository;
            _hasher = hasher;
        }

        public async Task<CreateUsuarioModel> CreateUsuarioAsync(CreateUsuarioModel model)
        {
            var usuarios = new Usuario
            {
                RolId = 16, // Asignar el RolId directamente
                EmpresaId = model.EmpresaId,
                Nombre = model.Nombre,
                Email = model.Email,
                PasswordHash = _hasher.Hash(model.PasswordHash)
            };

            var createdUsuario = await _repository.CreateUsuarioAsync(usuarios);

            if (createdUsuario == null)
                return null;

            return new CreateUsuarioModel
            {
                EmpresaId = createdUsuario.EmpresaId,
                Nombre = createdUsuario.Nombre,
                Email = createdUsuario.Email,
                PasswordHash = createdUsuario.PasswordHash
            };
        }

        public async Task<GetUsuarioModel> DeleteUsuarioAsync(int Id)
        {
            var usuario = await _repository.DeleteUsuarioAsync(Id);

            if (usuario == null)
                return null;

            return new GetUsuarioModel
            {
                Id = usuario.Id,
                RolId = usuario.RolId,
                EmpresaId = usuario.EmpresaId,
                Nombre = usuario.Nombre,
                Email = usuario.Email
            };
        }

        public async Task<List<GetUsuarioModel>> GetAllUsuarioAsync()
        {
            var usuarios = await _repository.GetAllUsuariosAsync();

            if (usuarios == null)
                return null;

            return usuarios.Select(u => new GetUsuarioModel
            {
                Id = u.Id,
                RolId = u.RolId,
                EmpresaId = u.EmpresaId,
                Nombre = u.Nombre,
                Email = u.Email
            }).ToList();
        }

        public async Task<LoginRequestModel> GetEmailForLogin(string email)
        {
            var usuario = await _repository.GetEmailForLogin(email);

            if (usuario == null)
            {
                return null;
            }

            return new LoginRequestModel
            {
                Email = usuario.Email,
                Password = usuario.PasswordHash
            };
        }

        public async Task<List<string>> GetRolesByUsuarioIdAsync(int usuarioId)
        {
            var usuario = await _repository.GetRolesByUsuarioIdAsync(usuarioId);

            if (usuario == null)
            {
                return null;
            }

            return usuario;
        }

        public async Task<GetUsuarioModel> GetUsuarioByEmailAsync(string email)
        {
            var usuario = await _repository.GetUsuarioByEmailAsync(email);

            if (usuario == null)
            {
                return null;
            }
            
            return new GetUsuarioModel
            {
                Id = usuario.Id,
                RolId = usuario.RolId,
                EmpresaId = usuario.EmpresaId,
                Nombre = usuario.Nombre,
                Email = usuario.Email
            };
        }

        public async Task<GetUsuarioModel> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _repository.GetUsuarioByIdAsync(id);

            if (usuario == null)
            {
                return null;
            }
            
            return new GetUsuarioModel
            {
                Id = usuario.Id,
                RolId = usuario.RolId,
                EmpresaId = usuario.EmpresaId,
                Nombre = usuario.Nombre,
                Email = usuario.Email
            };
        }

        public async Task<UpdateUsuarioModel> UpdateUsuarioAsync(UpdateUsuarioModel entity)
        {
            var usuario = new Usuario
            {
                Id = entity.Id,
                EmpresaId = entity.EmpresaId,
                Nombre = entity.Nombre,
                Email = entity.Email,
                PasswordHash = _hasher.Hash(entity.PasswordHash)
            };

            var updatedUsuario = await _repository.UpdateUsuarioAsync(usuario);

            if (updatedUsuario == null)
                return null;

            return new UpdateUsuarioModel
            {
                Id = updatedUsuario.Id,
                EmpresaId = updatedUsuario.EmpresaId,
                Nombre = updatedUsuario.Nombre,
                Email = updatedUsuario.Email,
                PasswordHash = updatedUsuario.PasswordHash
            };
        }
    }
}
