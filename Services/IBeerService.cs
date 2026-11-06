using System;
using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services;

public interface IBeerService
{
    Task<IEnumerable<BeerDto>> Get();
   // Task<ActionResult<BeerDto>> GetById(int id);
    Task<BeerDto> GetById(int id);
   // Task<ActionResult<BeerDto>> Add(BeerInsertDto dto);
    Task<BeerDto> Add(BeerInsertDto dto);
    //Task<IActionResult> Update(int id, BeerUpdateDto dto);  
    Task<BeerDto> Update(int id, BeerUpdateDto dto);  
    //Task<IActionResult> Delete(int id);
    Task<BeerDto> Delete(int id);
}
