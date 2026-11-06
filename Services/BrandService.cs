using System;
using Backend.DTOs;
using Backend.Models;
using Backend.Repository;

namespace Backend.Services;

public class BrandService : ICommonService<BrandDto, BrandInsertDto, BrandUpdateDto>
{
    private IRepository<Brand> _brandRepository;
    private IRepository<Beer> _beerRepository;

    public BrandService(IRepository<Brand> brandRepository, IRepository<Beer> beerRepository)
    {
        _brandRepository = brandRepository;
        _beerRepository = beerRepository;

    }



    public async Task<IEnumerable<BrandDto>> Get()
    {
        var brands = await _brandRepository.Get();
        return brands.Select(
            b => new BrandDto
            {
                Id = b.BrandId,
                Name = b.Name
            }
        );
    }

    public async Task<BrandDto> GetById(int id)
    {
        var brand = await _brandRepository.GetById(id);
        var result = new BrandDto
        {
            Id = brand.BrandId,
            Name = brand.Name
        };

        if (result == null)
        {
            return null;
        }
        return result;
    }
    public async Task<BrandDto> Add(BrandInsertDto dto)
    {
        var exists = await _brandRepository.ExistsByName(dto.Name);

        if (exists)
        {
            throw new Exception("Ya existe una marca con ese nombre.");
        }

        var brand = new Brand
        {
            Name = dto.Name
        };



        await _brandRepository.Add(brand);
        await _brandRepository.Save();
        //_context.Beers.Add(beer);


        var result = new BrandDto
        {
            Id = brand.BrandId,
            Name = brand.Name,
        };

        return result;

    }

    public async Task<BrandDto> Update(int id, BrandUpdateDto dto)
    {
        var brand = await _brandRepository.GetById(id);
        if (brand == null)
        {
            return null;
        }

        brand.Name = dto.Name;

        _brandRepository.Update(brand);
        await _brandRepository.Save();

        var brandDto = new BrandDto
        {
            Id = brand.BrandId,
            Name = brand.Name,
        };

        return brandDto;

    }
    public async Task<BrandDto> Delete(int id)
    {
        var brand = await _brandRepository.GetById(id);

        if (brand == null)
        {
            return null;
        }

        _brandRepository.Delete(brand);
        await _brandRepository.Save();

        var brandDto = new BrandDto
        {
            Id = brand.BrandId,
            Name = brand.Name
        };

        return brandDto;
    }

    public async Task<bool> HasBeers(int brandId)
    {
        return await _beerRepository.HasBeersByBrandId(brandId);
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
}
