using SGFE.Domein.Entitys.ReportesEntirys;

namespace SGFE.Domein.Interfaces.Reportes
{
    public interface IReporteRepository
    {
        Task<List<FacturaRepostes>> GetFacturaRepostesAsync(FacturaRepostes filtro);
        Task<List<ResumenFacturas>> GetResumenFacturasAsync(ResumenFacturas filtro);
    }
}
