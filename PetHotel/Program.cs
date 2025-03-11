using Microsoft.OpenApi.Models;
using PetHotel.API.Extensions;
using PetHotel.API.Middlewares;
using PetHotel.Application.Extensions;
using PetHotel.Domain.Entities;
using PetHotel.Infrastructure.Extensions;
using PetHotel.Infrastructure.Seeders;
using Serilog;

namespace PetHotel.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
           
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();
            builder.AddPresentation();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();

            await seeder.Seed();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.MapGroup("api/identity").MapIdentityApi<User>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
