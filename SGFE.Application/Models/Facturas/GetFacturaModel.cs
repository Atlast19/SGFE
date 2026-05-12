

namespace SGFE.Application.Models.Facturas
{
    public class GetFacturaModel
    {
        public int Id { get; set; }
        public string NCF { get; set; }
        public int TipoECFId { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal ItbisTotal { get; set; }
        public decimal SubTotal { get; set; }
        public string ClienteNombre { get; set; }

        public string ClienteDocumento { get; set; }

        public string TiposECF { get; set; }

        public List<GetFacturaDetalleModel> Detalles { get; set; }
    }
}
