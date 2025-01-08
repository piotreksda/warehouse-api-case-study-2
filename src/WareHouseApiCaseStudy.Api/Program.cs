using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WareHouseApiCaseStudy.Api.Application.Auth;
using WareHouseApiCaseStudy.Api.Application.Product;
using WareHouseApiCaseStudy.Api.Application.WareHouse;
using WareHouseApiCaseStudy.Api.Dtos;
using WareHouseApiCaseStudy.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS_LOCALHOST", builder =>
    {
        builder.WithOrigins("https://localhost:5174") // Specify the allowed origin
            .AllowAnyHeader()                   // Allow any headers
            .AllowAnyMethod();                  // Allow any methods (GET, POST, etc.)
    });
});

// Add services for EF Core and Identity
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("WarehouseDb"));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

app.UseCors("CORS_LOCALHOST");

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference(); // scalar/v1
    app.MapOpenApi();
}

app.MapAuthEndpoints()
    .MapProductEndpoints()
    .MapWarehouseEndpoints();

app.Run();

internal partial class Program;