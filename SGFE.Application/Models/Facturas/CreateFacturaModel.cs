
namespace SGFE.Application.Models.Facturas
{
    public class CreateFacturaModel
    {
        public int CertificadoId { get; set; }
        public int EmpresaId { get; set; }
        public int ClienteId { get; set; }
        public int TipoECFId { get; set; }
        public List<CreateFacturaDetalleModel> Detalles { get; set; }
    }
}
