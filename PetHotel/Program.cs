using Microsoft.OpenApi.Models;
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

            builder.Services.AddControllers();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                        new OpenApiSecurityScheme
                             {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
                             },
                        []
                     }

                });
            });

            builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
                
            builder.Services.AddScoped<ErrorHandlingMiddleware>();

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
