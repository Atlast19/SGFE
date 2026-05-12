
using FluentValidation;
using SGFE.Application.Models.Facturas;

namespace SGFE.Application.Validations.Facturas
{
    public class CreateFacturaDetalleValidation : AbstractValidator<CreateFacturaDetalleModel>
    {
        public CreateFacturaDetalleValidation()
        {
            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres.");

            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero.");

            RuleFor(x => x.PrecioUnitario)
                .GreaterThan(0).WithMessage("El precio unitario debe ser mayor a cero.");
        }
    }
}
