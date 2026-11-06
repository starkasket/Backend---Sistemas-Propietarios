using System;
using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services;

//public interface IBeerService
public interface ICommonService<T, TI, TU>
{
    Task<IEnumerable<T>> Get();
    Task<T> GetById(int id);
    Task<T> Add(TI dto);
    Task<T> Update(int id, TU dto);  
    Task<T> Delete(int id);

    Task<bool> HasBeers(int brandId);
    Task<IEnumerable<BeerDto>> GetBeersByBrand(int brandId);
}
