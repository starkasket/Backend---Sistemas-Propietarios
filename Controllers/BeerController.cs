using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {


        //public StoreContext _context;

        private IValidator<BeerInsertDto> _beerInsertValidator;
        private IValidator<BeerUpdateDto> _beerUpdateValidator;
        private readonly ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> _commonService;
        //private readonly IBeerService _beerService;

        //public BeerController(StoreContext context,
        public BeerController(
        IValidator<BeerInsertDto> beerInsertValidator,
        IValidator<BeerUpdateDto> beerUpdateValidator,
        [FromKeyedServices("beerService")] ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> commonService)
        {
            // _context = context;
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
            _commonService = commonService;
        }

        [HttpGet]
        //public async Task<IEnumerable<BeerDto>> Get() => await _beerService.Get();
        public async Task<IEnumerable<BeerDto>> Get() => await _commonService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beer = await _commonService.GetById(id);

            return beer == null ? NotFound() : Ok(beer);
        }


        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto dto)
        {
            var validationResult = await _beerInsertValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beer = await _commonService.Add(dto);

            return CreatedAtAction(nameof(GetById), new { id = beer.Id }, beer);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BeerUpdateDto dto)
        {
            /*  if (id != dto.Id)
             {
                 return BadRequest("El ID de la URL no coincide con el ID del objeto.");
             } */

            var validationResult = await _beerUpdateValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beer = await _commonService.Update(id, dto);

            return beer == null ? NotFound() : Ok(beer);


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> Delete(int id)
        {
            var beer = await _commonService.Delete(id);

            if (beer == null)
            {
                return NotFound();
            }

            return Ok(beer);
        }


    }
}
