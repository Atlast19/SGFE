
using FluentValidation;
using SGFE.Application.Models.CertificadosDigitales;

namespace SGFE.Application.Validations.CertificadosDigitales
{
    public class CretateCertificadoDigitalValidation : AbstractValidator<CreateCertificadoDigitalModel>
    {
        public CretateCertificadoDigitalValidation()
        {
            RuleFor(x => x.NombreArchivo)
                .NotEmpty().WithMessage("El nombre del archivo es obligatorio.")
                .MaximumLength(255).WithMessage("El nombre del archivo no puede exceder 255 caracteres.");

            RuleFor(x => x.RutaArchivo)
                .NotEmpty().WithMessage("La ruta del archivo es obligatoria.")
                .MaximumLength(500).WithMessage("La ruta del archivo no puede exceder 500 caracteres.");

            RuleFor(x => x.ArchivoCertificado)
                .NotNull().WithMessage("El archivo del certificado es obligatorio.")
                .Must(x => x.Length > 0).WithMessage("El archivo del certificado no puede estar vacío.");

            RuleFor(x => x.PasswordEncriptada)
                .NotNull().WithMessage("La contraseña encriptada es obligatoria.")
                .Must(x => x.Length > 0).WithMessage("La contraseña encriptada no puede estar vacía.");

            RuleFor(x => x.FechaVencimiento)
                .NotNull().WithMessage("La fecha de vencimiento es obligatoria.")
                .GreaterThan(DateTime.UtcNow.Date)
                .WithMessage("La fecha de vencimiento debe ser mayor a la fecha actual.");
        }
    }
}
