

namespace SGFE.Domein.Entitys.ReportesEntirys
{
    public class FacturaRepostes
    {
        public int Id { get; set; }
        public string NCF { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal MontoTotal { get; set; }
        public string Estado { get; set; }
        public string TrackId { get; set; }
        public string Cliente { get; set; }
        public string ClienteDocumento { get; set; }
        public string TipoComprobante { get; set; }
    }
}
