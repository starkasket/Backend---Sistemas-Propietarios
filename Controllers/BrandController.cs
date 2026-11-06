using Backend.DTOs;
using Backend.Repository;
using Backend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private IValidator<BrandInsertDto> _brandInsertValidator;
        private IValidator<BrandUpdateDto> _brandUpdateValidator;

        private readonly ICommonService<BrandDto, BrandInsertDto, BrandUpdateDto> _commonService;

        public BrandController(
            IValidator<BrandInsertDto> brandInsertValidator,
            IValidator<BrandUpdateDto> brandUpdateValidator,
            [FromKeyedServices("brandService")] ICommonService<BrandDto, BrandInsertDto, BrandUpdateDto> commonService
        )
        {
            _brandInsertValidator = brandInsertValidator;
            _brandUpdateValidator = brandUpdateValidator;
            _commonService = commonService;
        }

        [HttpGet]
        public async Task<IEnumerable<BrandDto>> Get() => await _commonService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandDto>> GetById(int id)
        {
            var brand = await _commonService.GetById(id);
            return brand == null ? NotFound() : Ok(brand);
        }

        [HttpPost]
        public async Task<ActionResult<BrandDto>> Add(BrandInsertDto dto)
        {
            var validationResult = await _brandInsertValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var brand = await _commonService.Add(dto);

            return CreatedAtAction(nameof(GetById), new { id = brand.Id }, brand);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BrandUpdateDto dto)
        {
            var validationResult = await _brandUpdateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);

            }
            var brand = await _commonService.Update(id, dto);
            return brand == null ? NotFound() : Ok(brand);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BrandDto>> Delete(int id)
        {
            var hasBeers = await _commonService.HasBeers(id);

            if (hasBeers)
            {
                return Conflict("No se puede eliminar una marca que tiene cervezas asociadas.");
            }
            var brand = await _commonService.Delete(id);

            return brand == null ? NotFound() : Ok(brand);
        }

        [HttpGet("{id}/beers")]
        public async Task<ActionResult<IEnumerable<BeerDto>>> GetBeersByBrand(int id)
        {
            var beers = await _commonService.GetBeersByBrand(id);

            return Ok(beers);
        }


    }
}
