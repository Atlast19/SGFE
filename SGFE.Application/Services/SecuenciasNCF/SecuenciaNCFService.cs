using SGFE.Application.Interfaces.SecuenciasNCF;
using SGFE.Application.Models.SecuenciaNCF;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.SecuenciasNCF;

namespace SGFE.Application.Services.SecuenciasNCF
{
    public class SecuenciaNCFService : ISecuenciaNCFService
    {
        private readonly ISecuenciaNCFRepository _repository;

        public SecuenciaNCFService(ISecuenciaNCFRepository repository)
        {
            _repository = repository;
        }

        public async Task RegistrarAsync(RegistrarSecuenciaNCFModel model)
        {
            // Validaciones importantes
            if (model.RangoInicio >= model.RangoFin)
                throw new Exception("El rango es inválido");

            if (model.VigenciaDesde > model.VigenciaHasta)
                throw new Exception("La vigencia es inválida");

            var entity = new SecuenciaNCF
            {
                EmpresaId = model.EmpresaId,
                TipoECFId = model.TipoECFId,
                Prefijo = model.Prefijo,
                RangoInicio = model.RangoInicio,
                RangoFin = model.RangoFin,
                VigenciaDesde = model.VigenciaDesde,
                VigenciaHasta = model.VigenciaHasta,
                SecuenciaActual = model.RangoInicio,
                Activo = true
            };

            await _repository.RegistrarSecuenciaAsync(entity);
        }
    }
}
