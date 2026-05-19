using SGFE.Application.Models.Repostes;


namespace SGFE.Application.Interfaces.Reportes
{
    public interface IReporteService
    {
        Task<List<GetFacturaReposte>> GetFacturaRepostesAsync(GetFacturaReporteFiltroModel filtroModel);
    }
}
