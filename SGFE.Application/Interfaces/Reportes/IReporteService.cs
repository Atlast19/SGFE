

using SGFE.Domein.Entitys.ReportesEntirys;

namespace SGFE.Application.Interfaces.Reportes
{
    public interface IReporteService
    {
        Task<List<FacturaRepostes>> GetFacturaRepostesAsync(int empresaId, DateTime? fechaDesde, DateTime? fechaHasta, string estado);
        Task<List<ResumenFacturas>> GetResumenFacturasAsync(int empresaId, int? anio, int? mes);
    }
}
