using System;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class BeerRepository : IRepository<Beer>
{
    private StoreContext _context;

    public BeerRepository(StoreContext context)
    {
        _context = context;
    }
    public async Task Add(Beer entity) => await _context.Beers.AddAsync(entity);

    public async Task Save() => await _context.SaveChangesAsync();

    public async Task<IEnumerable<Beer>> Get() => await _context.Beers.ToListAsync();

    public async Task<Beer> GetById(int id) => await _context.Beers.FindAsync(id);

    public void Update(Beer entity)
    {
        _context.Beers.Attach(entity);
        _context.Beers.Entry(entity).State = EntityState.Modified;
    }
    public void Delete(Beer entity) => _context.Beers.Remove(entity);

    public Task<bool> ExistsByName(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> HasBeersByBrandId(int brandId)
    {
        return await _context.Beers
            .AnyAsync(b => b.BrandId == brandId);
    }

    public async Task<IEnumerable<Beer>> GetByBrandId(int brandId)
    {
        return await _context.Beers
            .Where(b => b.BrandId == brandId)
            .ToListAsync();
    }
}
