using SGFE.Application.Interfaces.Empresas;
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
        public async Task CreateEmpresaAsync(Empresa entiry)
        {
            await _repository.CreateEmpresaAsync(entiry);
        }

        public async Task<List<Empresa>> GetAllEmpresaAsync()
        {
            return await _repository.GetAllEmpresaAsync();
        }

        public async Task<Empresa> GetEmpresaByIdAsync(int empresaId)
        {
            return await _repository.GetEmpresaByIdAsync(empresaId);
        }

        public async Task UpdateEmpresaAsync(Empresa entity)
        {
            await _repository.UpdateEmpresaAsync(entity);
        }
    }
}
