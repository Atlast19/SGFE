
using FluentValidation;
using SGFE.Application.Models.Usuarios;

namespace SGFE.Application.Validations.Usuarios
{
    public class CreateUsuarioValidation : AbstractValidator<CreateUsuarioModel>
    {
        public CreateUsuarioValidation()
        {
            RuleFor(x => x.EmpresaId)
                .GreaterThan(0).WithMessage("La empresa es obligatoria.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(60).WithMessage("El nombre no puede exceder 60 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El correo electrónico no es válido.")
                .MaximumLength(100).WithMessage("El correo electrónico no puede exceder 100 caracteres.");

            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .MaximumLength(100).WithMessage("La contraseña no puede exceder 100 caracteres.");
        }
    }
}
