
namespace SGFE.Application.Models.Clientes
{
    public class UpdateClienteModel
    {
        public int Id { get; set; }

        public string TipoDocumento { get; set; }

        public string Documento { get; set; }

        public string Nombre { get; set; }

        public string NombreComercial { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }
    }
}
