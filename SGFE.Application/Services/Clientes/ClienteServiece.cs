using SGFE.Application.Interfaces.Clientes;
using SGFE.Application.Models.Clientes;
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

        public async Task<CreateClienteModel> CreateClienteAsync(CreateClienteModel model)
        {
            var clientes = new Cliente
            {
                EmpresaId = model.EmpresaId,
                TipoDocumento = model.TipoDocumento,
                Documento = model.Documento,
                Nombre = model.Nombre,
                NombreComercial =model.NombreComercial,
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                Email = model.Email
            };

            var CreateCliente = await _repository.CreateClienteAsync(clientes);

            if (CreateCliente == null)
                return null;

            return new CreateClienteModel
            {
                EmpresaId = CreateCliente.EmpresaId,
                TipoDocumento = CreateCliente.TipoDocumento,
                Documento = CreateCliente.Documento,
                Nombre = CreateCliente.Nombre,
                NombreComercial = CreateCliente.NombreComercial,
                Direccion = CreateCliente.Direccion,
                Telefono = CreateCliente.Telefono,
                Email = CreateCliente.Email
            };
        }

        public async Task<GetClienteModel> DeleteClienteAsync(int Id)
        {
            var clientes = await _repository.DeleteClienteAsync(Id);

            if (clientes == null)
                return null;

            return new GetClienteModel
            {
                Id = clientes.Id,
                EmpresaId = clientes.EmpresaId,
                TipoDocumento = clientes.TipoDocumento,
                Documento = clientes.Documento,
                Nombre = clientes.Nombre,
                NombreComercial = clientes.NombreComercial,
                Direccion = clientes.Direccion,
                Telefono = clientes.Telefono,
                Email = clientes.Email
            };
        }

        public async Task<List<GetClienteModel>> GetAllClienteAsync()
        {
            var clientes = await _repository.GetAllClienteAsync();

            if (clientes == null)
                return null;

            return clientes.Select(c => new GetClienteModel 
            {
                Id = c.Id,
                EmpresaId = c.EmpresaId,
                TipoDocumento = c.TipoDocumento,
                Documento = c.Documento,
                Nombre = c.Nombre,
                NombreComercial = c.NombreComercial,
                Direccion = c.Direccion,
                Telefono = c.Telefono,
                Email = c.Email
            }).ToList();
        }

        public async Task<GetClienteModel> GetClienteByEmpresaIdAsync(int EmpresaId)
        {
            var clientes = await _repository.GetClienteByEmpresaIdAsync(EmpresaId);

            if (clientes == null)
                return null;

            return new GetClienteModel
            {
                Id = clientes.Id,
                EmpresaId = clientes.EmpresaId,
                TipoDocumento = clientes.TipoDocumento,
                Documento = clientes.Documento,
                Nombre = clientes.Nombre,
                NombreComercial = clientes.NombreComercial,
                Direccion = clientes.Direccion,
                Telefono = clientes.Telefono,
                Email = clientes.Email
            };
        }

        public async Task<GetClienteModel> GetClienteByIdAsync(int ClienteId)
        {
            var clientes = await _repository.GetClienteByIdAsync(ClienteId);

            if (clientes == null)
                return null;

            return new GetClienteModel
            {
                Id = clientes.Id,
                EmpresaId = clientes.EmpresaId,
                TipoDocumento = clientes.TipoDocumento,
                Documento = clientes.Documento,
                Nombre = clientes.Nombre,
                NombreComercial = clientes.NombreComercial,
                Direccion = clientes.Direccion,
                Telefono = clientes.Telefono,
                Email = clientes.Email
            };
        }

        public async Task<UpdateClienteModel> UpdateClienteAsync(UpdateClienteModel model)
        {
            var clientes = new Cliente
            {
                Id = model.Id,
                TipoDocumento= model.TipoDocumento,
                Documento = model.Documento,
                Nombre = model.Nombre,
                NombreComercial = model.NombreComercial,
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                Email = model.Email
            };

            var UpdateCliente = await _repository.UpdateClienteAsync(clientes);

            if (UpdateCliente == null)
                return null;

            return new UpdateClienteModel
            {
                Id = UpdateCliente.Id,
                TipoDocumento = UpdateCliente.TipoDocumento,
                Documento = UpdateCliente.Documento,
                Nombre = UpdateCliente.Nombre,
                NombreComercial = UpdateCliente.NombreComercial,
                Direccion = UpdateCliente.Direccion,
                Telefono = UpdateCliente.Telefono,
                Email = UpdateCliente.Email
            };
        }
    }
}
