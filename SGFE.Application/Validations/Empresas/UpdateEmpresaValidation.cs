
using FluentValidation;
using SGFE.Application.Models.Empresas;

namespace SGFE.Application.Validations.Empresas
{
    public class UpdateEmpresaValidation : AbstractValidator<UpdateEmpresaModel>
    {
        public UpdateEmpresaValidation()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El identificador es obligatorio.");

            RuleFor(x => x.RNC)
                .NotEmpty().WithMessage("El RNC es obligatorio.")
                .MaximumLength(20).WithMessage("El RNC no puede exceder 20 caracteres.");

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
