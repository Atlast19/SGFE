using SGFE.Application.Interfaces.CertificadosDigitales;
using SGFE.Application.Models.CertificadosDigitales;
using SGFE.Domein.Entitys;
using SGFE.Domein.Interfaces.CertificadosDigitales;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SGFE.Application.Services.CertificadosDigitales
{
    public class CertificadoDigicalService : ICertificadoDigitalService
    {
        private readonly ICertificadosDigitalRepository _repository;

        public CertificadoDigicalService(ICertificadosDigitalRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetCertificadoDigitalModel> GetCertificadoDigitalByIdAsync(int id)
        {
            var entity = await _repository.GetCertificadoDigitalByIdAsync(id);

            if (entity == null)
                return null;

            return new GetCertificadoDigitalModel
            {
                NombreArchivo = entity.NombreArchivo,
                RutaArchivo = entity.RutaArchivo,
                ArchivoCertificado = entity.ArchivoCertificado,
                PasswordEncriptada = entity.PasswordEncriptada,
                FechaVencimiento = entity.FechaVencimiento
            };
        }

        public async Task UploadCertificadoDigitalAsync(CreateCertificadoDigitalModel model)
        {
            try
            {
                var password = Encoding.UTF8.GetString(model.PasswordEncriptada);

                var cert = new X509Certificate2(model.ArchivoCertificado, password);
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
