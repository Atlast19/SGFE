

namespace SGFE.Application.Models.SecuenciaNCF
{
    public class RegistrarSecuenciaNCFModel
    {
        public int EmpresaId { get; set; }
        public int TipoECFId { get; set; }
        public string Prefijo { get; set; }
        public int RangoInicio { get; set; }
        public int RangoFin { get; set; }
        public DateTime VigenciaDesde { get; set; }
        public DateTime VigenciaHasta { get; set; }
    }
}
