using SGFE.Application.Models.CertificadosDigitales;

namespace SGFE.Application.Interfaces.CertificadosDigitales
{
    public interface ICertificadoDigitalService
    {
        Task UploadCertificadoDigitalAsync(CreateCertificadoDigitalModel model);
        Task<GetCertificadoDigitalModel> GetCertificadoDigitalByIdAsync(int id);
    }
}
