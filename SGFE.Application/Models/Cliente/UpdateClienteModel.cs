
namespace SGFE.Application.Models.Cliente
{
    public class UpdateClienteModel
    {
        public int Id { get; set; }

        public int EmpresaId { get; set; }

        public string Nombre { get; set; }

        public string RncOCedula { get; set; }

        public string TipoDocumento { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }
    }
}
