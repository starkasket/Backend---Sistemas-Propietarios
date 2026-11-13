using Backend.DTOs;
using Backend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private IValidator<InventoryInsertDto> _inventoryInsertValidator;
        private IValidator<InventoryUpdateDto> _inventoryUpdateValidator;

        private readonly ICommonService<InventoryDto, InventoryInsertDto, InventoryUpdateDto> _commonService;

        public InventoryController(
            IValidator<InventoryInsertDto> inventoryInsertValidator,
            IValidator<InventoryUpdateDto> inventoryUpdateValidator,
            [FromKeyedServices("inventoryService")] ICommonService<InventoryDto, InventoryInsertDto, InventoryUpdateDto> commonService
        )
        {
            _inventoryInsertValidator = inventoryInsertValidator;
            _inventoryUpdateValidator = inventoryUpdateValidator;
            _commonService = commonService;
        }

        [HttpGet]
        public async Task<IEnumerable<InventoryDto>> Get() => await _commonService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryDto>> GetById(int id)
        {
            var inventory = await _commonService.GetById(id);
            return inventory == null ? NotFound() : Ok(inventory);
        }

        [HttpPost]
        public async Task<ActionResult<InventoryDto>> Add(InventoryInsertDto dto)
        {
            var validationResult = await _inventoryInsertValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var inventory = await _commonService.Add(dto);

            return CreatedAtAction(nameof(GetById), new { id = inventory.InventoryId }, inventory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, InventoryUpdateDto dto)
        {
            var validationResult = await _inventoryUpdateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);

            }
            var inventory = await _commonService.Update(id, dto);
            return inventory == null ? NotFound() : Ok(inventory);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<InventoryDto>> Delete(int id)
        {
           /*  var hasBeers = await _commonService.HasBeers(id);

            if (hasBeers)
            {
                return Conflict("No se puede eliminar una marca que tiene cervezas asociadas.");
            } */
            var inventory = await _commonService.Delete(id);

            return inventory == null ? NotFound() : Ok(inventory);
        }

      /*   [HttpGet("{id}/beers")]
        public async Task<ActionResult<IEnumerable<BeerDto>>> GetBeersByBrand(int id)
        {
            var beers = await _commonService.GetBeersByBrand(id);

            return Ok(beers);
        } */

    }
}
