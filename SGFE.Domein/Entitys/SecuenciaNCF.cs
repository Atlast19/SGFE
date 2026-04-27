
namespace SGFE.Domein.Entitys 
{
    public class SecuenciaNCF
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int TipoECFId { get; set; }
        public int SecuenciaActual { get; set; }
        public string Prefijo { get; set; }
        public int RangoInicio { get; set; }
        public int RangoFin { get; set; }
        public DateOnly VigenciaDesde { get; set; }
        public DateOnly VigenciaHasta { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual TipoECF TipoECF { get; set; }
    }
}