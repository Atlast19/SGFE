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

        public async Task<List<GetFacturaReposte>> GetFacturaRepostesAsync(GetFacturaReposte filtroModel)
        {
            var filtro = new FacturaRepostes
            {
                EmpresaId = filtroModel.EmpresaId,
                FechaDesde = filtroModel.FechaDesde,
                FechaHasta = filtroModel.FechaHasta
            };

            var facturaRepostesEntities = await _repository.GetFacturaRepostesAsync(filtro);

            if (facturaRepostesEntities == null)
                return null;

            return facturaRepostesEntities.Select(entity => new GetFacturaReposte
            {
                EmpresaId = entity.EmpresaId,
                FechaDesde = entity.FechaDesde,
                FechaHasta = entity.FechaHasta
            }).ToList();
        }

        public async Task<List<GetResumenFactura>> GetResumenFacturasAsync(GetResumenFactura filtroModel)
        {
            var filtro = new ResumenFacturas
            {
                EmpresaId = filtroModel.EmpresaId,
                Mes = filtroModel.Mes,
                Anio = filtroModel.Anio
            };

            var resumenFacturasEntities = await _repository.GetResumenFacturasAsync(filtro);

            if(resumenFacturasEntities == null)
                return null;

            return resumenFacturasEntities.Select(entity => new GetResumenFactura
            {
                EmpresaId = entity.EmpresaId,
                Mes = entity.Mes,
                Anio = entity.Anio
            }).ToList();
        }
    }
}
