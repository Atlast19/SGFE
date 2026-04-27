
namespace SGFE.Domein.Entitys 
{
    public class Configuracion
    {
        public int Id { get; set; }
        public int? EmpresaId { get; set; }
        public string Clave { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}