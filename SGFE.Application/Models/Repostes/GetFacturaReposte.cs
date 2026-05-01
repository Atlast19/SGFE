
namespace SGFE.Application.Models.Repostes
{
    public class GetFacturaReposte
    {
        public int EmpresaId { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
    }
}
