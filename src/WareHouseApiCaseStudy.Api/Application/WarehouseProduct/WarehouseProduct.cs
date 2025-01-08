using WareHouseApiCaseStudy.Api.Application.WareHouse;

namespace WareHouseApiCaseStudy.Api.Application.WarehouseProduct;

public class WarehouseProduct
{
    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = null!;

    public Guid ProductId { get; set; }
    public Product.Product Product { get; set; } = null!;
}