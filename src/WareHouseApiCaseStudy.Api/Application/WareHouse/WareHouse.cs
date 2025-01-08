namespace WareHouseApiCaseStudy.Api.Application.WareHouse;

public class Warehouse
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;

    // Relacja wiele-do-wielu
    public ICollection<WarehouseProduct.WarehouseProduct> WarehouseProducts { get; set; } = [];
}