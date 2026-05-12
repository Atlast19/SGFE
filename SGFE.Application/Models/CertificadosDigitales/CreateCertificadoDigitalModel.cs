

namespace SGFE.Application.Models.CertificadosDigitales
{
    public class CreateCertificadoDigitalModel
    {
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public byte[] ArchivoCertificado { get; set; }
        public byte[] PasswordEncriptada { get; set; }
        public DateTime? FechaVencimiento { get; set; }
    }
}
