
using FluentValidation;
using SGFE.Application.Models.TiposECF;

namespace SGFE.Application.Validations.TiposECF
{
    public class CreateTipoECFValidation : AbstractValidator<CreateTipoECFModel>
    {
        public CreateTipoECFValidation()
        {
            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("El código es obligatorio.")
                .MaximumLength(3).WithMessage("El código no puede exceder 3 caracteres.");

            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MaximumLength(200).WithMessage("La descripción no puede exceder 200 caracteres.");
        }
    }
}
