using System;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class SaleRepository : IRepository<Sale>
{
    private StoreContext _context;

    public SaleRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task Add(Sale entity) => await _context.Sales.AddAsync(entity);
    public async Task Save() => await _context.SaveChangesAsync();

    public async Task<IEnumerable<Sale>> Get() => await _context.Sales.ToListAsync();

    public async Task<Sale> GetById(int id) => await _context.Sales.FindAsync(id);
    public void Update(Sale entity)
    {
        _context.Sales.Attach(entity);
        _context.Sales.Entry(entity).State = EntityState.Modified;
    }
    public void Delete(Sale entity) => _context.Sales.Remove(entity);

    public Task<bool> ExistsByName(string name)
    {
        throw new NotImplementedException();
    }


    public Task<IEnumerable<Sale>> GetByBrandId(int brandId)
    {
        throw new NotImplementedException();
    }


    public Task<bool> HasBeersByBrandId(int brandId)
    {
        throw new NotImplementedException();
    }


}
