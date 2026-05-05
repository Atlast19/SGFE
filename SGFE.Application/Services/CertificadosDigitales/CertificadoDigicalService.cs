using SGFE.Application.Interfaces.CertificadosDigitales;
using SGFE.Application.Models.CertificadosDigitales;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.CertificadosDigitales;
using System.Security.Cryptography.X509Certificates;

namespace SGFE.Application.Services.CertificadosDigitales
{
    public class CertificadoDigicalService : ICertificadoDigitalService
    {
        private readonly ICertificadosDigitalRepository _repository;

        public CertificadoDigicalService(ICertificadosDigitalRepository repository)
        {
            _repository = repository;
        }
        public async Task UploadCertificadoDigitalAsync(CreateCertificadoDigitalModel model)
        {
            try
            {
                var cert = new X509Certificate2(model.ArchivoCertificado, model.PasswordEncriptada);
            }
            catch
            {
                throw new Exception("Certificado o password inválido");
            }

            var entity = new CertificadosDigital
            {
                NombreArchivo = model.NombreArchivo,
                RutaArchivo = model.RutaArchivo,
                ArchivoCertificado = model.ArchivoCertificado,
                PasswordEncriptada = model.PasswordEncriptada,
                FechaVencimiento = model.FechaVencimiento,
                Activo = true
            };

            await _repository.UploadCertificadoDigitalAsync(entity);
        }
    }
}
