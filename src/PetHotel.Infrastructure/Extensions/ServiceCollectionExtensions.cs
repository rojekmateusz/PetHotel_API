using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetHotel.Domain.Entities;
using PetHotel.Domain.Interfaces;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;
using PetHotel.Infrastructure.Authorization.Services;
using PetHotel.Infrastructure.Configuration;
using PetHotel.Infrastructure.Persistance;
using PetHotel.Infrastructure.Repositories;
using PetHotel.Infrastructure.Seeders;
using PetHotel.Infrastructure.Storage;
using System.Runtime.CompilerServices;

namespace PetHotel.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PetHotelDb");
        services.AddDbContext<PetHotelDbContext>(options => options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<PetHotelDbContext>();

        services.AddScoped<IAnimalRepository, AnimalRepository>();
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<ISeeder, Seeder>();
        services.AddScoped<IOwnerAuthorizationService, OwnerAuthorizationService>();
        services.AddScoped<IHotelAuthorizationService, HotelAuthorizationService>();
        services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));
        services.AddScoped<IBlobStorageService, BlobStorageService>();
    }
}
