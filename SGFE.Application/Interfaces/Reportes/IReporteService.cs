using SGFE.Application.Models.Repostes;


namespace SGFE.Application.Interfaces.Reportes
{
    public interface IReporteService
    {
        Task<List<GetFacturaReposte>> GetFacturaRepostesAsync(GetFacturaReposte filtroModel);
        Task<List<GetResumenFactura>> GetResumenFacturasAsync(GetResumenFactura filtroModel);
    }
}
