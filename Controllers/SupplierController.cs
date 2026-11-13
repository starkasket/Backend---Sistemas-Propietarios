using Backend.DTOs;
using Backend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private IValidator<SupplierInsertDto> _supplierInsertValidator;
        private IValidator<SupplierUpdateDto> _supplierUpdateValidator;

        private readonly ICommonService<SupplierDto, SupplierInsertDto, SupplierUpdateDto> _commonService;

        public SupplierController(
            IValidator<SupplierInsertDto> supplierInsertValidator,
            IValidator<SupplierUpdateDto> supplierUpdateValidator,
            [FromKeyedServices("supplierService")] ICommonService<SupplierDto, SupplierInsertDto, SupplierUpdateDto> commonService
        )
        {
            _supplierInsertValidator = supplierInsertValidator;
            _supplierUpdateValidator = supplierUpdateValidator;
            _commonService = commonService;
        }

        [HttpGet]
        public async Task<IEnumerable<SupplierDto>> Get() => await _commonService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto>> GetById(int id)
        {
            var supplier = await _commonService.GetById(id);
            return supplier == null ? NotFound() : Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult<SupplierDto>> Add(SupplierInsertDto dto)
        {
            var validationResult = await _supplierInsertValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var supplier = await _commonService.Add(dto);

            return CreatedAtAction(nameof(GetById), new { id = supplier.SupplierId }, supplier);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SupplierUpdateDto dto)
        {
            var validationResult = await _supplierUpdateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);

            }
            var supplier = await _commonService.Update(id, dto);
            return supplier == null ? NotFound() : Ok(supplier);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SupplierDto>> Delete(int id)
        {
           
            var supplier = await _commonService.Delete(id);

            return supplier == null ? NotFound() : Ok(supplier);
        }
    }
}
