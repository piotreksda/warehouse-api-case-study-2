using Microsoft.AspNetCore.Mvc.Testing;
using WareHouseApiCaseStudy.Api.Application.Product;
using WareHouseApiCaseStudy.Api.Application.WareHouse;

namespace ApiTests;

public class ApiTests
{
    private readonly WebApplicationFactory<Program> api;

    public ApiTests()
    {
        api = new WebApplicationFactory<Program>();
    }
    
    [Fact]
    public async Task CreateWarehouseTest()
    {
        // Arrange: Setup API client and payload
        var client = api.CreateClient();
        var body = new Warehouse()
        {
            Id = Guid.NewGuid(),
            Name = "test",
            Location = "test123"
        };
        // Convert the body to JSON format
        var jsonContent = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(body),
            System.Text.Encoding.UTF8,
            "application/json"
        );
        // Act: Send POST request
        var response = await client.PostAsync("warehouses", jsonContent);
        // Assert: Verify response status code and other details
        response.EnsureSuccessStatusCode(); // Asserts 2xx status code
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
    }
    
    

}