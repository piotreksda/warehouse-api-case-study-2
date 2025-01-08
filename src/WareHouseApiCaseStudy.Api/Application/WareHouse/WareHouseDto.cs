namespace WareHouseApiCaseStudy.Api.Application.WareHouse;

public record WareHouseDto(Guid Id, string Name, string Location, List<WareHouseProductDto> Products);