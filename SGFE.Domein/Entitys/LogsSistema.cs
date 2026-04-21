
namespace SGFE.Domein.Entitys;

public class LogsSistema
{
    public int Id { get; set; }

    public string Tipo { get; set; }

    public string Mensaje { get; set; }

    public string Detalle { get; set; }

    public DateTime? Fecha { get; set; }

    public int? ReferenciaId { get; set; }
}