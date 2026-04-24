using SGFE.Application.Interfaces.Reportes;
using SGFE.Domein.Entitys.ReportesEntirys;
using SGFE.Domein.Interfaces;

namespace SGFE.Application.Services.Reportes
{
    public class ReporteService : IReporteService
    {
        private readonly IResporteRepository _repository;

        public ReporteService(IResporteRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FacturaRepostes>> GetFacturaRepostesAsync(int empresaId, DateTime? fechaDesde, DateTime? fechaHasta, string estado)
        {
            return await _repository.GetFacturaRepostesAsync(empresaId, fechaDesde, fechaHasta, estado);
        }

        public async Task<List<ResumenFacturas>> GetResumenFacturasAsync(int empresaId, int? anio, int? mes)
        {
            return await _repository.GetResumenFacturasAsync(empresaId, anio, mes);
        }
    }
}
