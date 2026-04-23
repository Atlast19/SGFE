

using SGFE.Domein.Entitys;

namespace SGFE.Domein.Interfaces
{
    public interface ICertificadosDigitalesRepository
    {
        Task CrearCertificadoDigitalAsync(CertificadosDigital entoty);

        Task<CertificadosDigital> GetEstadoCertificadoDigalAsync(int EmpresaID);
    }
}
 