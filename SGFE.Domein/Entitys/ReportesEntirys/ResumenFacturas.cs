

namespace SGFE.Domein.Entitys.ReportesEntirys
{
    public class ResumenFacturas
    {
        public int Anio { get; set; }
        public int Mes { get; set; }
        public string Estado { get; set; }
        public int Cantidad { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal ItbisTotal { get; set; }
    }
}
