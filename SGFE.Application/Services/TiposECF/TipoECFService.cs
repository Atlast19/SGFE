using SGFE.Application.Interfaces.TiposECF;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.TiposECF;

namespace SGFE.Application.Services.TiposECF
{
    public class TipoECFService : ITipoECFService
    {
        private readonly ITipoECFRepository _repository;

        public TipoECFService(ITipoECFRepository repository)
        {
            _repository = repository;
        }
        public async Task CrearTiposECFAsync(TipoECF entity)
        {
            await _repository.CrearTiposECFAsync(entity);
        }

        public async Task<List<TipoECF>> GetAllTiposECFAsync()
        {
            return await _repository.GetAllTiposECFAsync();
        }
    }
}
