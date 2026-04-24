using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces.Clientes
{
    public interface IClienteRepository
    {
        Task CreateClienteAsync(Cliente entity);
        Task UpdateClienteAsync (Cliente entity);
        Task<Cliente> GetClienteByEmpresaIdAsync(int EmpresaId);
        Task<Cliente> GetClienteByIdAsync(int ClienteId);
        Task<List<Cliente>> GetAllClienteAsync();
        Task DeleteClienteAsync(int Id);



    }
}
