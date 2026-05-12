using FluentValidation;
using SGFE.Application.Models.SecuenciaNCF;

namespace SGFE.Application.Validations.SecuenciaNCF
{
    public class ResgistrarSecuenciaNCFValidation : AbstractValidator<RegistrarSecuenciaNCFModel>
    {
        public ResgistrarSecuenciaNCFValidation()
        {
            RuleFor(x => x.EmpresaId)
                .GreaterThan(0).WithMessage("La empresa es obligatoria.");

            RuleFor(x => x.TipoECFId)
                .GreaterThan(0).WithMessage("El tipo de ECF es obligatorio.");

            RuleFor(x => x.Prefijo)
                .NotEmpty().WithMessage("El prefijo es obligatorio.")
                .MaximumLength(10).WithMessage("El prefijo no puede exceder 10 caracteres.");

            RuleFor(x => x.RangoInicio)
                .GreaterThan(0).WithMessage("El rango de inicio debe ser mayor a cero.");

            RuleFor(x => x.RangoFin)
                .GreaterThan(0).WithMessage("El rango final debe ser mayor a cero.")
                .GreaterThan(x => x.RangoInicio)
                .WithMessage("El rango final debe ser mayor que el rango inicial.");

            RuleFor(x => x.VigenciaDesde)
                .NotEmpty().WithMessage("La vigencia desde es obligatoria.");

            RuleFor(x => x.VigenciaHasta)
                .NotEmpty().WithMessage("La vigencia hasta es obligatoria.")
                .GreaterThan(x => x.VigenciaDesde)
                .WithMessage("La vigencia hasta debe ser mayor que la vigencia desde.");
        }
    }
}
