using System;
using Backend.DTOs;
using Backend.Models;
using Backend.Repository;

namespace Backend.Services;

public class InventoryService : ICommonService<InventoryDto, InventoryInsertDto, InventoryUpdateDto>
{

    private IRepository<Inventory> _inventoryRepository;
    private IRepository<Beer> _beerRepository;

    public InventoryService(IRepository<Inventory> inventoryRepository, IRepository<Beer> beerRepository)
    {
        _inventoryRepository = inventoryRepository;
        _beerRepository = beerRepository;
    }
    public async Task<IEnumerable<InventoryDto>> Get()
    {
        var inventories = await _inventoryRepository.Get();
        return inventories.Select(i => new InventoryDto
        {
            InventoryId = i.InventoryId,
            Quantity = i.Quantity,
            BatchNumber = i.BatchNumber,
            BeerId = i.BeerId,
            ProductionDate = i.ProductionDate,
            StorageSection = i.StorageSection,
            PurchasePrice = i.PurchasePrice,
            BestBeforeDate = i.BestBeforeDate,
        });
    }
    public async Task<InventoryDto> GetById(int id)
    {
        var inventory = await _inventoryRepository.GetById(id);
        var result = new InventoryDto
        {
            InventoryId = inventory.InventoryId,
            Quantity = inventory.Quantity,
            BatchNumber = inventory.BatchNumber,
            BeerId = inventory.BeerId,
            ProductionDate = inventory.ProductionDate,
            StorageSection = inventory.StorageSection,
            PurchasePrice = inventory.PurchasePrice,
            BestBeforeDate = inventory.BestBeforeDate,
        };

        if (result == null)
        {
            return null;
        }
        return result;
    }

    public async Task<InventoryDto> Add(InventoryInsertDto dto)
    {

        var inventory = new Inventory
        {
            Quantity = dto.Quantity,
            BatchNumber = dto.BatchNumber,
            BeerId = dto.BeerId,
            ProductionDate = dto.ProductionDate,
            StorageSection = dto.StorageSection,
            PurchasePrice = dto.PurchasePrice,
            BestBeforeDate = dto.BestBeforeDate
        };

        await _inventoryRepository.Add(inventory);
        await _inventoryRepository.Save();

        var result = new InventoryDto
        {
            InventoryId = inventory.InventoryId,
            Quantity = inventory.Quantity,
            BatchNumber = inventory.BatchNumber,
            BeerId = inventory.BeerId,
            ProductionDate = inventory.ProductionDate,
            StorageSection = inventory.StorageSection,
            PurchasePrice = inventory.PurchasePrice,
            BestBeforeDate = inventory.BestBeforeDate
        };

        return result;

    }
    public async Task<InventoryDto> Update(int id, InventoryUpdateDto dto)
    {
        var inventory = await _inventoryRepository.GetById(id);

        if (inventory == null)
        {
            return null;
        }

        inventory.Quantity = dto.Quantity;
        inventory.BatchNumber = dto.BatchNumber;
        inventory.BeerId = dto.BeerId;
        inventory.ProductionDate = dto.ProductionDate;
        inventory.StorageSection = dto.StorageSection;
        inventory.PurchasePrice = dto.PurchasePrice;
        inventory.BestBeforeDate = dto.BestBeforeDate;

        _inventoryRepository.Update(inventory);
        await _inventoryRepository.Save();

        var inventoryDto = new InventoryDto
        {
            InventoryId = inventory.InventoryId,
            Quantity = inventory.Quantity,
            BatchNumber = inventory.BatchNumber,
            BeerId = inventory.BeerId,
            ProductionDate = inventory.ProductionDate,
            StorageSection = inventory.StorageSection,
            PurchasePrice = inventory.PurchasePrice,
            BestBeforeDate = inventory.BestBeforeDate,
        };

        return inventoryDto;

    }

    public async Task<InventoryDto> Delete(int id)
    {
        var inventory = await _inventoryRepository.GetById(id);

        if (inventory == null)
        {
            return null;
        }

        _inventoryRepository.Delete(inventory);
        await _inventoryRepository.Save();

        var inventoryDto = new InventoryDto
        {
            InventoryId = inventory.InventoryId,
            Quantity = inventory.Quantity,
            BatchNumber = inventory.BatchNumber,
            BeerId = inventory.BeerId,
            ProductionDate = inventory.ProductionDate,
            StorageSection = inventory.StorageSection,
            PurchasePrice = inventory.PurchasePrice,
            BestBeforeDate = inventory.BestBeforeDate,
        };

        return inventoryDto;
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
