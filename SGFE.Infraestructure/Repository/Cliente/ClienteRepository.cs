using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Interfaces;

namespace SGFE.Percistence.Repository.Cliente
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ClienteRepository> _logger;
        private readonly string _connectionString;

        public ClienteRepository(IConfiguration configuration, ILogger<ClienteRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public Task CreateClienteAsync(Domein.Entitys.Cliente entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteClienteAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Domein.Entitys.Cliente>> GetAllClienteAsync(string SPname)
        {
            throw new NotImplementedException();
        }

        public Task<Domein.Entitys.Cliente> GetClienteByEmpresaIdAsync(int EmpresaId)
        {
            throw new NotImplementedException();
        }

        public Task<Domein.Entitys.Cliente> GetClienteByIdAsync(int ClienteId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateClienteAsync(Domein.Entitys.Cliente entity)
        {
            throw new NotImplementedException();
        }
    }
}
