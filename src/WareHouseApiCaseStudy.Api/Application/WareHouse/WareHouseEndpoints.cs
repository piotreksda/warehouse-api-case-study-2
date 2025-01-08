using Microsoft.EntityFrameworkCore;
using WareHouseApiCaseStudy.Api.Application.Mapper;
using WareHouseApiCaseStudy.Api.Infrastructure;

namespace WareHouseApiCaseStudy.Api.Application.WareHouse;

public static class WarehouseEndpoints
{
    public static IEndpointRouteBuilder MapWarehouseEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/warehouses", async (AppDbContext dbContext) =>
        {
            var warehouses = await dbContext.Warehouses.Include(w => w.WarehouseProducts)
                .ThenInclude(wp => wp.Product)
                .ToListAsync();
            return Results.Ok(warehouses.Select(x => x.MapToDto()));
        });

        endpoints.MapGet("/warehouses/{id:guid}", async (Guid id, AppDbContext dbContext) =>
        {
            var warehouse = await dbContext.Warehouses
                .Include(w => w.WarehouseProducts)
                .ThenInclude(wp => wp.Product)
                .FirstOrDefaultAsync(w => w.Id == id);

            return warehouse is not null ? Results.Ok(warehouse.MapToDto()) : Results.NotFound();
        });

        endpoints.MapPost("/warehouses", async (Warehouse warehouse, AppDbContext dbContext) =>
        {
            dbContext.Warehouses.Add(warehouse);
            await dbContext.SaveChangesAsync();
            return Results.Created($"/warehouses/{warehouse.Id}", warehouse);
        });

        endpoints.MapPut("/warehouses/{id:guid}", async (Guid id, Warehouse updatedWarehouse, AppDbContext dbContext) =>
        {
            var warehouse = await dbContext.Warehouses.FindAsync(id);
            if (warehouse is null)
            {
                return Results.NotFound();
            }

            warehouse.Name = updatedWarehouse.Name;
            warehouse.Location = updatedWarehouse.Location;
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        endpoints.MapDelete("/warehouses/{id:guid}", async (Guid id, AppDbContext dbContext) =>
        {
            var warehouse = await dbContext.Warehouses.FindAsync(id);
            if (warehouse is null)
            {
                return Results.NotFound();
            }

            dbContext.Warehouses.Remove(warehouse);
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });
        
        endpoints.MapPut("/warehouses/{warehouseId:guid}/products/{productId:guid}/add", async (Guid warehouseId, Guid productId, AppDbContext dbContext) =>
        {
            var warehouse = await dbContext.Warehouses.FindAsync(warehouseId);
            var product = await dbContext.Products.FindAsync(productId);

            if (warehouse is null || product is null)
            {
                return Results.NotFound();
            }

            // Check if the association already exists
            var existingAssociation = await dbContext.WarehouseProducts
                .FirstOrDefaultAsync(wp => wp.WarehouseId == warehouseId && wp.ProductId == productId);

            if (existingAssociation is not null)
            {
                return Results.Ok($"Product {productId} is already associated with Warehouse {warehouseId}.");
            }

            // Create the association
            var warehouseProduct = new WarehouseProduct.WarehouseProduct
            {
                WarehouseId = warehouseId,
                ProductId = productId
            };

            dbContext.WarehouseProducts.Add(warehouseProduct);
            await dbContext.SaveChangesAsync();

            return Results.Ok($"Product {productId} has been added to Warehouse {warehouseId}.");
        });
        
        endpoints.MapPut("/warehouses/{warehouseId:guid}/products/{productId:guid}/remove", async (Guid warehouseId, Guid productId, AppDbContext dbContext) =>
        {
            var warehouse = await dbContext.Warehouses.FindAsync(warehouseId);
            var product = await dbContext.Products.FindAsync(productId);

            if (warehouse is null || product is null)
            {
                return Results.NotFound();
            }

            // Find the association
            var warehouseProduct = await dbContext.WarehouseProducts
                .FirstOrDefaultAsync(wp => wp.WarehouseId == warehouseId && wp.ProductId == productId);

            if (warehouseProduct is null)
            {
                return Results.NotFound($"Product {productId} is not associated with Warehouse {warehouseId}.");
            }

            // Remove the association
            dbContext.WarehouseProducts.Remove(warehouseProduct);
            await dbContext.SaveChangesAsync();

            return Results.Ok($"Product {productId} has been removed from Warehouse {warehouseId}.");
        });
        
        return endpoints;
    }
}