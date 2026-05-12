
using FluentValidation;
using SGFE.Application.Models.Clientes;

namespace SGFE.Application.Validations.Clientes
{
    public class UpdateClienteValidation : AbstractValidator<UpdateClienteModel>
    {
        public UpdateClienteValidation()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El identificador es obligatorio.");

            RuleFor(x => x.TipoDocumento)
                .NotEmpty().WithMessage("El tipo de documento es obligatorio.")
                .Must(x => x == "Cedula" || x == "RNC" || x == "Pasaporte")
                .WithMessage("El tipo de documento debe ser: Cedula, RNC o Pasaporte.");

            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("El documento es obligatorio.")

                // Cédula Dominicana = 11 caracteres
                .Must((model, documento) =>
                {
                    if (model.TipoDocumento == "Cedula")
                        return documento.Length == 11;

                    return true;
                })
                .WithMessage("La cédula debe contener 11 caracteres.")

                // RNC = 9 caracteres
                .Must((model, documento) =>
                {
                    if (model.TipoDocumento == "RNC")
                        return documento.Length == 9;

                    return true;
                })
                .WithMessage("El RNC debe contener 9 caracteres.")

                // Pasaporte = entre 6 y 20 caracteres
                .Must((model, documento) =>
                {
                    if (model.TipoDocumento == "Pasaporte")
                        return documento.Length >= 6 && documento.Length <= 20;

                    return true;
                })
                .WithMessage("El pasaporte debe contener entre 6 y 20 caracteres.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(60).WithMessage("El nombre no puede exceder 60 caracteres.");

            RuleFor(x => x.NombreComercial)
                .MaximumLength(100).WithMessage("El nombre comercial no puede exceder 100 caracteres.");

            RuleFor(x => x.Direccion)
                .NotEmpty().WithMessage("La dirección es obligatoria.")
                .MaximumLength(200).WithMessage("La dirección no puede exceder 200 caracteres.");

            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El teléfono es obligatorio.")
                .MaximumLength(11).WithMessage("El teléfono no puede exceder 11 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El correo electrónico no es válido.")
                .MaximumLength(100).WithMessage("El correo electrónico no puede exceder 100 caracteres.");
        }
    }
}
