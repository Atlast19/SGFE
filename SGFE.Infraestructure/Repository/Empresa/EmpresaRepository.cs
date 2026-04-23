
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGFE.Domein.Interfaces;

namespace SGFE.Percistence.Repository.Empresa
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmpresaRepository> _logger;
        private readonly string _connectionString;

        public EmpresaRepository(IConfiguration configuration, ILogger<EmpresaRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public Task CreateEmpresaAsync(Domein.Entitys.Empresa entiry)
        {
            throw new NotImplementedException();
        }

        public Task<List<Domein.Entitys.Empresa>> GetAllEmpresaAsync(string SPname)
        {
            throw new NotImplementedException();
        }

        public Task<Domein.Entitys.Empresa> GetEmpresaByIdAsync(int empresaId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEmpresaAsync(Domein.Entitys.Empresa entity)
        {
            throw new NotImplementedException();
        }
    }
}
