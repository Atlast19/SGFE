
namespace SGFE.Domein.Entitys;

public class Log
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public int? EmpresaId { get; set; }

    public string Nivel { get; set; }

    public string Mensaje { get; set; }

    public string Detalle { get; set; }

    public string IP { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Empresa Empresa { get; set; }

    public virtual Usuario Usuario { get; set; }
}