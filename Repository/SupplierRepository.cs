using System;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class SupplierRepository : IRepository<Supplier>
{
    private StoreContext _context;

    public SupplierRepository(StoreContext context)
    {
        _context = context;
    }
    public async Task Add(Supplier entity) => await _context.Suppliers.AddAsync(entity);

    public async Task Save() => await _context.SaveChangesAsync();

    public async Task<IEnumerable<Supplier>> Get() => await _context.Suppliers.ToListAsync();
    public async Task<Supplier> GetById(int id) => await _context.Suppliers.FindAsync(id);

    public void Update(Supplier entity)
    {
        _context.Suppliers.Attach(entity);
        _context.Suppliers.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(Supplier entity) => _context.Suppliers.Remove(entity);

    public Task<bool> ExistsByName(string name)
    {
        throw new NotImplementedException();
    }


    public Task<IEnumerable<Supplier>> GetByBrandId(int brandId)
    {
        throw new NotImplementedException();
    }


    public Task<bool> HasBeersByBrandId(int brandId)
    {
        throw new NotImplementedException();
    }



}
