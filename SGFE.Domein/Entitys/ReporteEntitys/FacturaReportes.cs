
namespace SGFE.Domein.Entitys.ReportesEntirys
{
    public class FacturaReportes
    {
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        public string NCF { get; set; }

        public DateTime FechaEmision { get; set; }

        public decimal MontoTotal { get; set; }

        public decimal ItbisTotal { get; set; }

        public decimal SubTotal { get; set; }

        public string Estado { get; set; }

        public string TrackId { get; set; }

        public string Cliente { get; set; }

        public string ClienteDocumento { get; set; }

        public string TipoComprobante { get; set; }

        // RESUMEN FACTURAS
        public int Anio { get; set; }

        public int Mes { get; set; }

        public int CantidadFacturas { get; set; }

        public decimal TotalFacturado { get; set; }

        public decimal TotalItbis { get; set; }
    }
}