using System;
using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;


namespace Backend.Services;

//public class BeerService : IBeerService
public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
{
    /* private StoreContext _context; */
    private IRepository<Beer> _beerRepository;

    public BeerService(IRepository<Beer> beerRepository)
    {
        _beerRepository = beerRepository;
    }
    public async Task<IEnumerable<BeerDto>> Get()
    {
        var beers = await _beerRepository.Get();
        return beers.Select(
             b => new BeerDto
             {
                 Id = b.BeerId,
                 Name = b.Name,
                 Alcohol = b.Alcohol,
                 BrandId = b.BrandId
             }
         );
    }
    /*  public async Task<IEnumerable<BeerDto>> Get() => await _context.Beers.Select(
              b => new BeerDto
              {
                  Id = b.BeerId,
                  Name = b.Name,
                  Alcohol = b.Alcohol,
                  BrandId = b.BrandId
              }
          ).ToListAsync(); */

    // public Task<ActionResult<BeerDto>> GetById (int id){   }
    public async Task<BeerDto> GetById(int id)
    {

        var beer = await _beerRepository.GetById(id);
        /*   var beer = await _context.Beers
          .Where(b => b.BeerId == id)
          .Select(b => new BeerDto
          {
              Id = b.BeerId,
              Name = b.Name,
              Alcohol = b.Alcohol,
              BrandId = b.BrandId

          }).FirstOrDefaultAsync(); */

        /*  var beer = await _context.Beers.FindAsync(id) */
        var result = new BeerDto
        {
            Id = beer.BeerId,
            Name = beer.Name,
            Alcohol = beer.Alcohol,
            BrandId = beer.BrandId
        };

        if (result == null)
        {
            return null;
        }

        /* var newBeer =new BeerDto{
            Id = b.BeerId,
            Name = b.Name,
            Alcohol = b.Alcohol,
            BrandId = b.BrandId} 
            */

        return result;
    }

    public async Task<IEnumerable<BeerDto>> GetBeersByBrand(int brandId)
    {
        var beers = await _beerRepository.GetByBrandId(brandId);

        return beers.Select(b => new BeerDto
        {
            Id = b.BeerId,
            Name = b.Name,
            Alcohol = b.Alcohol,
            BrandId = b.BrandId
        });
    }

    public async Task<BeerDto> Add(BeerInsertDto dto)
    {
        var beer = new Beer
        {
            Name = dto.Name,
            Alcohol = dto.Alcohol,
            BrandId = dto.BrandId
        };

        await _beerRepository.Add(beer);
        await _beerRepository.Save();
        //_context.Beers.Add(beer);


        var result = new BeerDto
        {
            Id = beer.BeerId,
            Name = beer.Name,
            Alcohol = beer.Alcohol,
            BrandId = beer.BrandId
        };

        return result;
    }

    // public Task<IActionResult> Put (int id, BeerUpdateDto dto)
    public async Task<BeerDto> Update(int id, BeerUpdateDto dto)
    {
        var beer = await _beerRepository.GetById(id);
        if (beer == null)
        {
            return null;
        }

        beer.Name = dto.Name;
        beer.Alcohol = dto.Alcohol;
        beer.BrandId = dto.BrandId;

        _beerRepository.Update(beer);
        await _beerRepository.Save();

        var beerDto = new BeerDto
        {
            Id = beer.BeerId,
            Name = beer.Name,
            Alcohol = beer.Alcohol,
            BrandId = beer.BrandId
        };

        return beerDto;

    }
    public async Task<BeerDto> Delete(int id)
    {
        var beer = await _beerRepository.GetById(id);

        if (beer == null)
        {
            return null;
        }

        _beerRepository.Delete(beer);
        await _beerRepository.Save();

        var beerDto = new BeerDto
        {
            Id = beer.BeerId,
            Name = beer.Name,
            Alcohol = beer.Alcohol,
            BrandId = beer.BrandId
        };

        return beerDto;
    }

    public Task<bool> HasBeers(int brandId)
    {
        throw new NotImplementedException();
    }
}
