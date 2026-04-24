using SGFE.Application.Interfaces.Clientes;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.Clientes;

namespace SGFE.Application.Services.Clientes
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository) 
        {
            _repository = repository;
        }
        public async Task CreateClienteAsync(Cliente entity)
        {
            await _repository.CreateClienteAsync(entity);
        }

        public async Task DeleteClienteAsync(int Id)
        {
             await _repository.DeleteClienteAsync(Id);
        }

        public async Task<List<Cliente>> GetAllClienteAsync()
        {
            return await _repository.GetAllClienteAsync();
        }

        public async Task<Cliente> GetClienteByEmpresaIdAsync(int EmpresaId)
        {
            return await _repository.GetClienteByEmpresaIdAsync(EmpresaId);
        }

        public Task<Cliente> GetClienteByIdAsync(int ClienteId)
        {
            return _repository.GetClienteByIdAsync(ClienteId);
        }

        public async Task UpdateClienteAsync(Cliente entity)
        {
            await _repository.UpdateClienteAsync(entity);
        }
    }
}
