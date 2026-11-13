using System;
using Backend.DTOs;
using Backend.Models;
using Backend.Repository;

namespace Backend.Services;

public class SaleService : ICommonService<SaleDto, SaleInsertDto, SaleUpdateDto>
{
    private IRepository<Sale> _saleRepository;

    public SaleService(IRepository<Sale> saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<SaleDto> Add(SaleInsertDto dto)
    {
        var sale = new Sale
        {
            Quantity = dto.Quantity,
            UnitPrice = dto.UnitPrice,
            SaleDate = dto.SaleDate,
            ClientName = dto.ClientName,
            InventoryId = dto.InventoryId
        };

        await _saleRepository.Add(sale);
        await _saleRepository.Save();

        var result = new SaleDto
        {
            Quantity = sale.Quantity,
            UnitPrice = sale.UnitPrice,
            SaleDate = sale.SaleDate,
            ClientName = sale.ClientName,
            InventoryId = sale.InventoryId
        };
        return result;
    }

    public async Task<IEnumerable<SaleDto>> Get()
    {
        var sales = await _saleRepository.Get();
        return sales.Select(s => new SaleDto
        {
            SaleId = s.SaleId,
            Quantity = s.Quantity,
            UnitPrice = s.UnitPrice,
            SaleDate = s.SaleDate,
            ClientName = s.ClientName,
            InventoryId = s.InventoryId
        });
    }

    public async Task<SaleDto> GetById(int id)
    {
        var s = await _saleRepository.GetById(id);
        var result = new SaleDto
        {
            SaleId = s.SaleId,
            Quantity = s.Quantity,
            UnitPrice = s.UnitPrice,
            SaleDate = s.SaleDate,
            ClientName = s.ClientName,
            InventoryId = s.InventoryId
        };

        if (result == null)
        {
            return null;
        }
        return result;
    }

    public async Task<SaleDto> Update(int id, SaleUpdateDto dto)
    {
        var sale = await _saleRepository.GetById(id);

        if (sale == null)
        {
            return null;
        }

        sale.Quantity = dto.Quantity;
        sale.UnitPrice = dto.UnitPrice;
        sale.SaleDate = dto.SaleDate;
        sale.ClientName = dto.ClientName;
        sale.InventoryId = dto.InventoryId;

        _saleRepository.Update(sale);
        await _saleRepository.Save();

        var saleDto = new SaleDto
        {
            SaleId = sale.SaleId,
            Quantity = sale.Quantity,
            UnitPrice = sale.UnitPrice,
            SaleDate = sale.SaleDate,
            ClientName = sale.ClientName,
            InventoryId = sale.InventoryId
        };

        return saleDto;
    }

    public async Task<SaleDto> Delete(int id)
    {
        var sale = await _saleRepository.GetById(id);

        if (sale == null)
        {
            return null;
        }

        _saleRepository.Delete(sale);
        await _saleRepository.Save();

        var saleDto = new SaleDto
        {
            SaleId = sale.SaleId,
            Quantity = sale.Quantity,
            UnitPrice = sale.UnitPrice,
            SaleDate = sale.SaleDate,
            ClientName = sale.ClientName,
            InventoryId = sale.InventoryId
        };


        return saleDto;
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
