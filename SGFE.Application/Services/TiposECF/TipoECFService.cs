using SGFE.Application.Interfaces.TiposECF;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces;

namespace SGFE.Application.Services.TiposECF
{
    public class TipoECFService : ITipoECFService
    {
        private readonly ITipoECFRepository _repository;

        public TipoECFService(ITipoECFRepository repository)
        {
            _repository = repository;
        }
        public Task CrearTiposECFAsync(TipoECF entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<TipoECF>> GetAllTiposECFAsync()
        {
            throw new NotImplementedException();
        }
    }
}
