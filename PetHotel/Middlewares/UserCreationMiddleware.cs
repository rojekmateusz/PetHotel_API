
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;
using System.Text.Json;

namespace PetHotel.API.Middlewares;

public class UserCreationMiddleware(UserManager<User> userManager, ILogger<UserCreationMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next.Invoke(context);
                
        if (context.Request.Path == "/api/identity/register" && context.Request.Method == "POST")
        {
            context.Request.Body.Position = 0;
            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
      
            if (string.IsNullOrWhiteSpace(body))
            {
                logger.LogError("Request body is empty.");
                return;
            }
            
            var createUserRequest = JsonSerializer.Deserialize<RegisterRequest>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (createUserRequest != null)
            {
                var user = await userManager.FindByEmailAsync(createUserRequest.Email);

                if (user != null)
                {
                    await userManager.AddToRoleAsync(user, UserRoles.User);
                }
            }
        }
    }
}
