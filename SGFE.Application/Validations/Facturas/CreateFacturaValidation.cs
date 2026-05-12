
using FluentValidation;
using SGFE.Application.Models.Facturas;

namespace SGFE.Application.Validations.Facturas
{
    public class CreateFacturaValidation : AbstractValidator<CreateFacturaModel>
    {
        public CreateFacturaValidation()
        {
            RuleFor(x => x.CertificadoId)
                .GreaterThan(0).WithMessage("El certificado es obligatorio.");

            RuleFor(x => x.EmpresaId)
                .GreaterThan(0).WithMessage("La empresa es obligatoria.");

            RuleFor(x => x.ClienteId)
                .GreaterThan(0).WithMessage("El cliente es obligatorio.");

            RuleFor(x => x.TipoECFId)
                .GreaterThan(0).WithMessage("El tipo de ECF es obligatorio.");
        }
    }
}
