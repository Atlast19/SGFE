
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Interfaces;

namespace SGFE.Percistence.Repository.Usuario
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UsuarioRepository> _logger;
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration, ILogger<UsuarioRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public Task ActualizarUsuarioAsync(Domein.Entitys.Usuario entity)
        {
            throw new NotImplementedException();
        }

        public Task CreateUsuarioAsync(Domein.Entitys.Usuario entity)
        {
            throw new NotImplementedException();
        }

        public Task<Domein.Entitys.Usuario> GetUsuarioByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Domein.Entitys.Usuario> GetUsuarioByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
