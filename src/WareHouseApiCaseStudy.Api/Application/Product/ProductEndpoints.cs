using Microsoft.EntityFrameworkCore;
using WareHouseApiCaseStudy.Api.Application.Mapper;
using WareHouseApiCaseStudy.Api.Infrastructure;

namespace WareHouseApiCaseStudy.Api.Application.Product;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/products", async (AppDbContext dbContext) =>
        {
            var products = await dbContext.Products.ToListAsync();
            return Results.Ok(products.Select(x => x.MapToDto()));
        });

        endpoints.MapGet("/products/{id:guid}", async (Guid id, AppDbContext dbContext) =>
        {
            var product = await dbContext.Products.FindAsync(id);
            return product is not null ? Results.Ok(product.MapToDto()) : Results.NotFound();
        });

        endpoints.MapPost("/products", async (Product product, AppDbContext dbContext) =>
        {
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
            return Results.Created($"/products/{product.Id}", product);
        });

        endpoints.MapPut("/products/{id:guid}", async (Guid id, Product updatedProduct, AppDbContext dbContext) =>
        {
            var product = await dbContext.Products.FindAsync(id);
            if (product is null)
            {
                return Results.NotFound();
            }

            product.Name = updatedProduct.Name;
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        endpoints.MapDelete("/products/{id:guid}", async (Guid id, AppDbContext dbContext) =>
        {
            var product = await dbContext.Products.FindAsync(id);
            if (product is null)
            {
                return Results.NotFound();
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        return endpoints;
    }
}
