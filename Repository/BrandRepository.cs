using System;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository;

public class BrandRepository : IRepository<Brand>
{
    private StoreContext _context;

    public BrandRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task Add(Brand entity) => await _context.Brands.AddAsync(entity);


    public async Task Save() => await _context.SaveChangesAsync();

    public async Task<IEnumerable<Brand>> Get() => await _context.Brands.ToListAsync();
    public async Task<Brand> GetById(int id) => await _context.Brands.FindAsync(id);

    public void Update(Brand entity)
    {
        _context.Brands.Attach(entity);
        _context.Brands.Entry(entity).State = EntityState.Modified;
    }
    public void Delete(Brand entity) => _context.Brands.Remove(entity);

    public async Task<bool> ExistsByName(string name)
    {
        return await _context.Brands
            .AnyAsync(b => b.Name.ToLower() == name.ToLower());
    }

    public Task<bool> HasBeersByBrandId(int brandId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Brand>> GetByBrandId(int brandId)
    {
        throw new NotImplementedException();
    }
}
