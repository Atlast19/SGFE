

using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces
{
    public interface IClienteRepository
    {
        Task CreateClienteAsync(Cliente entity);
        Task UpdateClienteAsync (Cliente entity);
        Task GetClienteByEmpresaIdAsync(int EmpresaId);
        Task GetClienteByIdAsync(int ClienteId);
        Task<List<Cliente>> GetAllClienteAsync(string SPname);
        Task DeleteCliente(int Id);



    }
}
