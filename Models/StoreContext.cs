using System;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {

    }

    public DbSet<Beer> Beers { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Sale> Sales { get; set; }
}
