using System;
using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators;

public class SaleInsertValidator : AbstractValidator<SaleInsertDto>
{
    public SaleInsertValidator()
    {
        RuleFor(x => x.ClientName).NotEmpty().WithMessage("El nombre del cliente es obligatorio");
        RuleFor(x => x.ClientName).Length(2, 50).WithMessage("El nombre debe medir de 2 a 50 caracteres");
        RuleFor(x => x.Quantity).NotNull().WithMessage(x => "La cantidad es obligatoria").GreaterThan(0).WithMessage("La cantidad debe de ser mayor a 0");
        RuleFor(x => x.UnitPrice).NotNull().WithMessage(x => "El precio es obligatorio").GreaterThan(0).WithMessage(x => "Error con el valor enviado del precio");
        RuleFor(x => x.SaleDate).NotEmpty().WithMessage("La fecha de venta es obligatoria");
        RuleFor(x => x.InventoryId).NotNull().WithMessage("El identificador del inventario es obligatorio");

    }

}
