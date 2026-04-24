using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces.CertificadosDigitales
{
    public interface ICertificadosDigitalRepository
    {
        Task CrearCertificadoDigitalAsync(CertificadosDigital entoty);

        Task<CertificadosDigital> GetEstadoCertificadoDigalAsync(int EmpresaID);
    }
}
 