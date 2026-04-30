using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces.Clientes
{
    public interface IClienteRepository
    {
        Task<Cliente> CreateClienteAsync(Cliente entity);
        Task<Cliente> UpdateClienteAsync (Cliente entity);
        Task<Cliente> GetClienteByEmpresaIdAsync(int EmpresaId);
        Task<Cliente> GetClienteByIdAsync(int ClienteId);
        Task<List<Cliente>> GetAllClienteAsync();
        Task<Cliente> DeleteClienteAsync(int Id);



    }
}
