using WareHouseApiCaseStudy.Api.Application.Product;
using WareHouseApiCaseStudy.Api.Application.WareHouse;

namespace WareHouseApiCaseStudy.Api.Application.Mapper;

public static class ProductMapper
{
    public static ProductDto MapToDto(this Product.Product product)
    {
        return new ProductDto(product.Id, product.Name);
    }
    
    public static WareHouseProductDto MapToDto(this WarehouseProduct.WarehouseProduct warehouseProduct)
    {
        return new WareHouseProductDto(warehouseProduct.Warehouse.Id, warehouseProduct.Warehouse.Name, 0);
    }
    
    public static WareHouseDto MapToDto(this Warehouse wareHouse)
    {
        return new WareHouseDto(wareHouse.Id, wareHouse.Name, wareHouse.Location, wareHouse.WarehouseProducts.Select(x => x.MapToDto()).ToList());
    }

}