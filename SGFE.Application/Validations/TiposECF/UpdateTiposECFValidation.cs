
using FluentValidation;
using SGFE.Application.Models.TiposECF;

namespace SGFE.Application.Validations.TiposECF
{
    public class UpdateTiposECFValidation : AbstractValidator<UpdateTipoECFModel>
    {
        public UpdateTiposECFValidation()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El identificador es obligatorio.");

            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("El código es obligatorio.")
                .MaximumLength(3).WithMessage("El código no puede exceder 3 caracteres.");

            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MaximumLength(200).WithMessage("La descripción no puede exceder 200 caracteres.");
        }
    }
}
