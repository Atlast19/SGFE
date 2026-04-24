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
        public Task CreateUsuarioAsync(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> GetUsuarioByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> LoginAsync(string Email, string PasswordHash)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUsuarioAsync(Usuario entity)
        {
            throw new NotImplementedException();
        }
    }
}
