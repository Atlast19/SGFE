
using FluentValidation;
using SGFE.Application.Models.Usuarios;

namespace SGFE.Application.Validations.Usuarios
{
    public class LoginRequestValidation : AbstractValidator<LoginRequestModel>
    {
        public LoginRequestValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El correo electrónico no es válido.")
                .MaximumLength(100).WithMessage("El correo electrónico no puede exceder 100 caracteres.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .MaximumLength(100).WithMessage("La contraseña no puede exceder 100 caracteres.");
        }
    }
}
