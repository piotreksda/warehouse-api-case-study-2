using Microsoft.AspNetCore.Identity;
using WareHouseApiCaseStudy.Api.Dtos;

namespace WareHouseApiCaseStudy.Api.Application.Auth;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder endpoint)
    {
        // Register endpoint
        endpoint.MapPost("/register", async (UserDto userDto, UserManager<IdentityUser> userManager) =>
        {
            if (string.IsNullOrWhiteSpace(userDto.Username) || string.IsNullOrWhiteSpace(userDto.Password))
            {
                return Results.BadRequest("Username and password are required.");
            }

            var user = new IdentityUser { UserName = userDto.Username };
            var result = await userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {
                return Results.BadRequest(result.Errors);
            }

            return Results.Ok("User registered successfully.");
        });

        // Login endpoint
        endpoint.MapPost("/login", async (UserDto userDto, SignInManager<IdentityUser> signInManager) =>
        {
            var result = await signInManager.PasswordSignInAsync(userDto.Username, userDto.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return Results.Unauthorized();
            }

            return Results.Ok("Login successful.");
        });
        
        return endpoint;
    }
}