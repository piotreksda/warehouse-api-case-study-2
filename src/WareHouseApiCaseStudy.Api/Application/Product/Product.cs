namespace WareHouseApiCaseStudy.Api.Application.Product;

public class Product
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;

    // Relacja wiele-do-wielu
    public ICollection<WarehouseProduct.WarehouseProduct> WarehouseProducts { get; set; } = [];
}