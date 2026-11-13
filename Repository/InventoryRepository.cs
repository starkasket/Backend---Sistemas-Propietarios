using System;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

// dotnet ef migrations add AddInventory
// dotnet ef database update 

public class InventoryRepository : IRepository<Inventory>
{
    private StoreContext _context;

    public InventoryRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task Add(Inventory entity) => await _context.Inventories.AddAsync(entity);

    public async Task Save() => await _context.SaveChangesAsync();

    public async Task<IEnumerable<Inventory>> Get() => await _context.Inventories.ToListAsync();
    public async Task<Inventory> GetById(int id) => await _context.Inventories.FindAsync(id);
   
    public void Update(Inventory entity)
    {
        _context.Inventories.Attach(entity);
        _context.Inventories.Entry(entity).State = EntityState.Modified;
    }
    
    public void Delete(Inventory entity) => _context.Inventories.Remove(entity);



    public Task<bool> ExistsByName(string name)
    {
        throw new NotImplementedException();
    }


    public Task<IEnumerable<Inventory>> GetByBrandId(int brandId)
    {
        throw new NotImplementedException();
    }


    public Task<bool> HasBeersByBrandId(int brandId)
    {
        throw new NotImplementedException();
    }


}
