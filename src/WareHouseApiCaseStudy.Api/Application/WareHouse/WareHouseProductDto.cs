using WareHouseApiCaseStudy.Api.Application.Product;

namespace WareHouseApiCaseStudy.Api.Application.WareHouse;

public record WareHouseProductDto(Guid Id, string Name, decimal Count) : ProductDto(Id, Name);