
using SGFE.Application.Models.Clientes;

namespace SGFE.Application.Interfaces.Clientes
{
    public interface IClienteService
    {
        Task<CreateClienteModel> CreateClienteAsync(CreateClienteModel model);
        Task<UpdateClienteModel> UpdateClienteAsync(UpdateClienteModel model);
        Task<GetClienteModel> GetClienteByEmpresaIdAsync(int EmpresaId);
        Task<GetClienteModel> GetClienteByIdAsync(int ClienteId);
        Task<List<GetClienteModel>> GetAllClienteAsync();
        Task<GetClienteModel> DeleteClienteAsync(int Id);
    }
}
