using System;
using Backend.DTOs;
using Backend.Models;
using Backend.Repository;

namespace Backend.Services;

public class SupplierService : ICommonService<SupplierDto, SupplierInsertDto, SupplierUpdateDto>
{
    private IRepository<Supplier> _supplierRepository;

    private SupplierService(IRepository<Supplier> supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<SupplierDto> Add(SupplierInsertDto dto)
    {
        var supplier = new Supplier
        {
            Name = dto.Name,
            Country = dto.Country,
            Phone = dto.Country
        };

        await _supplierRepository.Add(supplier);
        await _supplierRepository.Save();

        var result = new SupplierDto
        {
            SupplierId = supplier.SupplierId,
            Name = supplier.Name,
            Country = supplier.Country,
            Phone = supplier.Phone
        };

        return result;

    }

    public async Task<IEnumerable<SupplierDto>> Get()
    {
        var suppliers = await _supplierRepository.Get();
        return suppliers.Select(s => new SupplierDto
        {
            SupplierId = s.SupplierId,
            Name = s.Name,
            Country = s.Country,
            Phone = s.Phone
        });
    }

    public async Task<SupplierDto> GetById(int id)
    {
        var supplier = await _supplierRepository.GetById(id);

        var result = new SupplierDto
        {
            SupplierId = supplier.SupplierId,
            Name = supplier.Name,
            Country = supplier.Country,
            Phone = supplier.Phone
        };

        if (result == null)
        {
            return null;
        }
        return result;
    }

    public async Task<SupplierDto> Update(int id, SupplierUpdateDto dto)
    {
        var supplier = await _supplierRepository.GetById(id);
        if (supplier == null)
        {
            return null;
        }

        supplier.Name = dto.Name;
        supplier.Country = dto.Country;
        supplier.Phone = dto.Phone;

        _supplierRepository.Update(supplier);
        await _supplierRepository.Save();

        var supplierDto = new SupplierDto
        {
            SupplierId = supplier.SupplierId,
            Name = supplier.Name,
            Country = supplier.Country,
            Phone = supplier.Phone
        };

        return supplierDto;

    }


    public async Task<SupplierDto> Delete(int id)
    {
        var supplier = await _supplierRepository.GetById(id);
        if (supplier == null)
        {
            return null;
        }

        _supplierRepository.Delete(supplier);
        await _supplierRepository.Save();

        var supplierDto = new SupplierDto
        {
            SupplierId = supplier.SupplierId,
            Name = supplier.Name,
            Country = supplier.Country,
            Phone = supplier.Phone
        };

        return supplierDto;
    }


    public Task<IEnumerable<BeerDto>> GetBeersByBrand(int brandId)
    {
        throw new NotImplementedException();
    }



    public Task<bool> HasBeers(int brandId)
    {
        throw new NotImplementedException();
    }


}
