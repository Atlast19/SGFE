using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces.CertificadosDigitales
{
    public interface ICertificadosDigitalRepository
    {
        Task UploadCertificadoDigitalAsync(CertificadosDigital entity);
        Task<CertificadosDigital> GetCertificadoDigitalByIdAsync(int id);

    }
}
 