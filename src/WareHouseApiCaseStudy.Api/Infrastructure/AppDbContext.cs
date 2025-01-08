using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WareHouseApiCaseStudy.Api.Application.Product;
using WareHouseApiCaseStudy.Api.Application.WareHouse;
using WareHouseApiCaseStudy.Api.Application.WarehouseProduct;

namespace WareHouseApiCaseStudy.Api.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Warehouse> Warehouses { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<WarehouseProduct> WarehouseProducts { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations
        modelBuilder.ApplyConfiguration(new WareHouseConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new WarehouseProductConfiguration());
    }
}