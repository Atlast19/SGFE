using SGFE.Domein.Entitys.ReportesEntirys;

namespace SGFE.Domein.Interfaces.Reportes
{
    public interface IReporteRepository
    {
        Task<List<FacturaRepostes>> GetFacturaRepostesAsync(int empresaId, DateTime? fechaDesde, DateTime? fechaHasta, string estado);
        Task<List<ResumenFacturas>> GetResumenFacturasAsync(int empresaId, int? anio, int? mes);
    }
}
