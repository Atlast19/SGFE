using SGFE.Application.Interfaces.Usuarios;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.Usuarios;

namespace SGFE.Application.Services.Ususarios
{
    public class UsuarioService : IUsuaroService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        public async Task CreateUsuarioAsync(Usuario entity)
        {
            await _repository.CreateUsuarioAsync(entity);
        }

        public async Task<Usuario> GetUsuarioByEmailAsync(string email)
        {
            return await _repository.GetUsuarioByEmailAsync(email);
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _repository.GetUsuarioByIdAsync(id);
        }

        public async Task UpdateUsuarioAsync(Usuario entity)
        {
            await _repository.UpdateUsuarioAsync(entity);
        }
    }
}
