using SGFE.Application.Interfaces.TiposECF;
using SGFE.Application.Models.TiposECF;
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

        public async Task<CreateTipoECF> CrearTiposECFAsync(CreateTipoECF model)
        {
            var tipoECFEntity = new TipoECF
            {
                Codigo = model.Codigo,
                Descripcion = model.Descripcion
            };

            var CreateTipoECF = await _repository.CrearTiposECFAsync(tipoECFEntity);

            if (CreateTipoECF == null)
                return null;

            return new CreateTipoECF
            {
                Codigo = CreateTipoECF.Codigo,
                Descripcion = CreateTipoECF.Descripcion
            };
        }

        public async Task<GetTipoECF> DeleteTipoECFAsync(int id)
        {
            var deletedTipoECF = await _repository.DeleteTipoECFAsync(id);

            if (deletedTipoECF == null)
                return null;

            return new GetTipoECF
            {
                Id = deletedTipoECF.Id,
                Codigo = deletedTipoECF.Codigo,
                Descripcion = deletedTipoECF.Descripcion
            };
        }

        public async Task<List<GetTipoECF>> GetAllTiposECFAsync()
        {
            var tiposECFEntities = await _repository.GetAllTiposECFAsync();

            if (tiposECFEntities == null)
                return null;

            return tiposECFEntities.Select(tipoECF => new GetTipoECF
            {
                Id = tipoECF.Id,
                Codigo = tipoECF.Codigo,
                Descripcion = tipoECF.Descripcion
            }).ToList();
        }

        public async Task<GetTipoECF> GetTipoECFByIdAsync(int id)
        {
            var tipoECFEntity = await _repository.GetTipoECFByIdAsync(id);

            if (tipoECFEntity == null)
                return null;

            return new GetTipoECF
            {
                Id = tipoECFEntity.Id,
                Codigo = tipoECFEntity.Codigo,
                Descripcion = tipoECFEntity.Descripcion
            };
        }

        public async Task<UpdateTipoECF> UpdateTipoECFAsync(UpdateTipoECF model)
        {
            var tipoECFEntity = new TipoECF
            {
                Id = model.Id,
                Codigo = model.Codigo,
                Descripcion = model.Descripcion
            };

            var updatedTipoECF = await _repository.UpdateTipoECFAsync(tipoECFEntity);

            if (updatedTipoECF == null)
                return null;

            return new UpdateTipoECF
            {
                Id = updatedTipoECF.Id,
                Codigo = updatedTipoECF.Codigo,
                Descripcion = updatedTipoECF.Descripcion
            };
        }
    }
}
