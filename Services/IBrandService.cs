using System;
using Backend.DTOs;

namespace Backend.Services;

public interface IBrandService :  ICommonService<BrandDto, BrandInsertDto, BrandUpdateDto>
{

    Task<IEnumerable<BrandDto>> Get();
    Task<BrandDto> GetById(int id);
    Task<BrandDto> Add(BrandInsertDto dto);
    Task<BrandDto> Update(int id, BrandUpdateDto dto);
    Task<BrandDto> Delete(int id);
}
