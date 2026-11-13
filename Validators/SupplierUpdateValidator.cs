using System;
using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators;

public class SupplierUpdateValidator : AbstractValidator<SupplierUpdateDto>
{
    public SupplierUpdateValidator()
    {
        RuleFor(x => x.SupplierId).NotNull().WithMessage("El Id es obligatorio");
        RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre del proveedor es obligatorio").Length(2, 50).WithMessage("El nombre debe tener una longitud de entre 2-50 caracteres");
        RuleFor(x => x.Country).NotEmpty().WithMessage("El país del proveedor es obligatorio").Length(2, 50).WithMessage("El país del proveedor debe tener una longitud de entre 2-50 caracteres");
        RuleFor(x => x.Phone).NotEmpty().WithMessage("El número de teléfono del proveedor es obligatorio").Length(7, 15).WithMessage("El número de teléfono debe estar compuesto entre 7-15 números");
    }
}
