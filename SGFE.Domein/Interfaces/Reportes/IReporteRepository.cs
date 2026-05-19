using SGFE.Domein.Entitys.ReportesEntirys;

namespace SGFE.Domein.Interfaces.Reportes
{
    public interface IReporteRepository
    {
        Task<List<FacturaReportes>> GetFacturaRepostesAsync(FacturaReportes filtro);
    }
}
