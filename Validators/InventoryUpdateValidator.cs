using System;
using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators;

public class InventoryUpdateValidator : AbstractValidator<InventoryUpdateDto>
{
       public InventoryUpdateValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("El Id es obligatorio ");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("La cantidad debe de ser mayor a 0");
        RuleFor(x => x.BatchNumber).GreaterThan(0).WithMessage("El número de lote debe ser mayor a 0");
        RuleFor(x => x.BeerId).NotNull().WithMessage("El identificador de la fecha es obligatorio.").GreaterThan(0).WithMessage("Error con el valor enviado de la cerveza");
        RuleFor(x => x.ProductionDate).NotEmpty().WithMessage("La fecha de producción es obligatoria.");
        RuleFor(x => x.PurchasePrice).GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor a 0.");
        RuleFor(x => x.BestBeforeDate).NotEmpty().WithMessage("La fecha de producción es obligatoria.").GreaterThan(x => x.ProductionDate).WithMessage("La fecha de consumo preferente debe ser posterior a la de producción");
        
    }
}
