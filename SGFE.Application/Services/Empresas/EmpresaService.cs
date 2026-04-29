using SGFE.Application.Interfaces.Empresas;
using SGFE.Application.Models.Empresas;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.Empresas;

namespace SGFE.Application.Services.Empresas
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _repository;

        public EmpresaService(IEmpresaRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateEmpresaModel> CreateEmpresaAsync(CreateEmpresaModel entiry)
        {
            var empresa = new Empresa
            {
                RNC = entiry.RNC,
                Nombre = entiry.Nombre,
                NombreComercial = entiry.NombreComercial,
                Direccion = entiry.Direccion,
                Telefono = entiry.Telefono,
                Email = entiry.Email
            };

            var createdEmpresa = await _repository.CreateEmpresaAsync(empresa);

            if (createdEmpresa == null)
                return null;

            return new CreateEmpresaModel
            {
                RNC = createdEmpresa.RNC,
                Nombre = createdEmpresa.Nombre,
                NombreComercial = createdEmpresa.NombreComercial,
                Direccion = createdEmpresa.Direccion,
                Telefono = createdEmpresa.Telefono,
                Email = createdEmpresa.Email
            };
        }

        public async Task<GetEmpresaModel> DeleteEmpresaAsync(int empresaId)
        {
            var empresa = await _repository.DeleteEmpresaAsync(empresaId);

            if (empresa == null)
                return null;

            return new GetEmpresaModel
            {
                Id = empresa.Id,
                RNC = empresa.RNC,
                Nombre = empresa.Nombre,
                NombreComercial = empresa.NombreComercial,
                Direccion = empresa.Direccion,
                Telefono = empresa.Telefono,
                Email = empresa.Email
            };
        }

        public async Task<List<GetEmpresaModel>> GetAllEmpresaAsync()
        {
            var empresas = await _repository.GetAllEmpresaAsync();

            if (empresas == null)
                return null;

            return empresas.Select(e => new GetEmpresaModel
            {
                Id = e.Id,
                Nombre = e.Nombre,
                RNC = e.RNC,
                NombreComercial = e.NombreComercial,
                Direccion = e.Direccion,
                Telefono = e.Telefono,
                Email = e.Email
            }).ToList();
        }

        public async Task<GetEmpresaModel> GetEmpresaByIdAsync(int empresaId)
        {
            var empresa = await _repository.GetEmpresaByIdAsync(empresaId);

            if (empresa == null)
                return null;

            return new GetEmpresaModel
            {
                Id = empresa.Id,
                Nombre = empresa.Nombre,
                RNC = empresa.RNC,
                NombreComercial = empresa.NombreComercial,
                Direccion = empresa.Direccion,
                Telefono = empresa.Telefono,
                Email = empresa.Email
            };
        }

        public async Task<GetEmpresaModel> UpdateEmpresaAsync(UpdateEmpresaModel entity)
        {
            var empresa = new Empresa
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                RNC = entity.RNC,
                NombreComercial = entity.NombreComercial,
                Direccion = entity.Direccion,
                Telefono = entity.Telefono,
                Email = entity.Email
            };

            var updatedEmpresa = await _repository.UpdateEmpresaAsync(empresa);

            if (updatedEmpresa == null)
                return null;

            return new GetEmpresaModel
            {
                Id = updatedEmpresa.Id,
                Nombre = updatedEmpresa.Nombre,
                RNC = updatedEmpresa.RNC,
                NombreComercial = updatedEmpresa.NombreComercial,
                Direccion = updatedEmpresa.Direccion,
                Telefono = updatedEmpresa.Telefono,
                Email = updatedEmpresa.Email
            };
        }
    }
}
