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
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Services.AddInfrastructure(builder.Configuration);
                builder.Services.AddApplication();
                builder.AddPresentation();

                var app = builder.Build();

                app.Use((context, next) =>
                {
                    context.Request.EnableBuffering();
                    return next();
                });

                var scope = app.Services.CreateScope();
                var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
                await seeder.Seed();


                // Configure the HTTP request pipeline.
                app.UseMiddleware<UserCreationMiddleware>();

                app.UseMiddleware<ErrorHandlingMiddleware>();

                app.UseSerilogRequestLogging();

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.MapGroup("api/identity").MapIdentityApi<User>();



                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }

            catch(Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }
    }
}
