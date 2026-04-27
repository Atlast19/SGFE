namespace SGFE.Domein.Entitys 
{
    public class CertificadosDigital
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public byte[] PasswordEncriptada { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}