using FluentValidation;
using SGFE.Application.Models.Roles;

namespace SGFE.Application.Validations.Roles
{
    public class CreateRolValidation : AbstractValidator<CreateRolModel>
    {
        public CreateRolValidation()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El identificador es obligatorio.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(60).WithMessage("El nombre no puede exceder 60 caracteres.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres.");
        }
    }
}