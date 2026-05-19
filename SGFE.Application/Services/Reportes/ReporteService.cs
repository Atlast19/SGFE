using SGFE.Application.Interfaces.Reportes;
using SGFE.Application.Models.Repostes;
using SGFE.Domein.Entitys.ReportesEntirys;
using SGFE.Domein.Interfaces.Reportes;

namespace SGFE.Application.Services.Reportes
{
    public class ReporteService : IReporteService
    {
        private readonly IReporteRepository _repository;

        public ReporteService(IReporteRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetFacturaReposte>> GetFacturaRepostesAsync(GetFacturaReporteFiltroModel filtroModel)
        {
                var filtroEntity = new FacturaReportes
                {
                    EmpresaId = filtroModel.EmpresaId,
                    FechaEmision = filtroModel.FechaEmision
                };

                var facturaEntities =
                    await _repository.GetFacturaRepostesAsync(filtroEntity);


                if (facturaEntities == null)
                    return null;


            var response = facturaEntities.Select(entity => new GetFacturaReposte
            {
                Id = entity.Id,
                EmpresaId = entity.EmpresaId,
                NCF = entity.NCF,
                FechaEmision = entity.FechaEmision,
                MontoTotal = entity.MontoTotal,
                ItbisTotal = entity.ItbisTotal,
                SubTotal = entity.SubTotal,
                Estado = entity.Estado,
                TrackId = entity.TrackId,
                Cliente = entity.Cliente,
                ClienteDocumento = entity.ClienteDocumento,
                TipoComprobante = entity.TipoComprobante,

                // RESUMEN
                Anio = entity.Anio,
                Mes = entity.Mes,
                CantidadFacturas = entity.CantidadFacturas,
                TotalFacturado = entity.TotalFacturado,
                TotalItbis = entity.TotalItbis

            }).ToList();



            return response;
        }
    }
}
