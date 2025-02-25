using Microsoft.OpenApi.Models;
using PetHotel.API.Extensions;
using PetHotel.API.Middlewares;
using PetHotel.Application.Extensions;
using PetHotel.Domain.Entities;
using PetHotel.Infrastructure.Extensions;
using Serilog;


namespace PetHotel.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
           
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();
            builder.AddPresentation();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

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
