using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Backend.Services;
using Backend.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddSingleton<IPeopleService, PeopleService>();
builder.Services.AddKeyedSingleton<IPeopleService, PeopleService>("peopleService");
builder.Services.AddKeyedSingleton<IPeopleService, PeopleService2>("peopleService2");
builder.Services.AddKeyedSingleton<IRandomService, RandomService>("randomSingleton");
builder.Services.AddKeyedScoped<IRandomService, RandomService>("randomScoped");
builder.Services.AddKeyedTransient<IRandomService, RandomService>("randomTransient");
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHttpClient<IPostService, PostService>(c => c.BaseAddress = new Uri(builder.Configuration["BaseUrlPost"]));
builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidator>();
builder.Services.AddScoped<IValidator<BeerUpdateDto>, BeerUpdateValidator>();

builder.Services.AddScoped<IValidator<BrandInsertDto>, BrandInsertValidator>();
builder.Services.AddScoped<IValidator<BrandUpdateDto>, BrandUpdateValidator>();

builder.Services.AddScoped<IValidator<InventoryInsertDto>, InventoryInsertValidator>();
builder.Services.AddScoped<IValidator<InventoryUpdateDto>, InventoryUpdateValidator>();

builder.Services.AddScoped<IValidator<SupplierInsertDto>, SupplierInsertValidator>();
builder.Services.AddScoped<IValidator<SupplierUpdateDto>, SupplierUpdateValidator>();

builder.Services.AddScoped<IValidator<SaleInsertDto>, SaleInsertValidator>();
builder.Services.AddScoped<IValidator<SaleUpdateDto>, SaleUpdateValidator>();



//builder.Services.AddScoped<IBeerService, BeerService>();
builder.Services.AddKeyedScoped<ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>, BeerService>("beerService");
builder.Services.AddKeyedScoped<ICommonService<BrandDto, BrandInsertDto, BrandUpdateDto>, BrandService>("brandService");
builder.Services.AddKeyedScoped<ICommonService<InventoryDto, InventoryInsertDto, InventoryUpdateDto>, InventoryService>("inventoryService");
builder.Services.AddKeyedScoped<ICommonService<SupplierDto, SupplierInsertDto, SupplierUpdateDto>, SupplierService>("supplierService");
builder.Services.AddKeyedScoped<ICommonService<SaleDto, SaleInsertDto, SaleUpdateDto>, SaleService>("saleService");

builder.Services.AddScoped<IRepository<Beer>, BeerRepository>();
builder.Services.AddScoped<IRepository<Brand>, BrandRepository>();
builder.Services.AddScoped<IRepository<Inventory>, InventoryRepository>();
builder.Services.AddScoped<IRepository<Supplier>, SupplierRepository>();
builder.Services.AddScoped<IRepository<Sale>, SaleRepository>();

builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
