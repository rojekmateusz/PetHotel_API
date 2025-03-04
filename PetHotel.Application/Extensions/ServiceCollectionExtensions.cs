using Microsoft.Extensions.DependencyInjection;
using PetHotel.Application.User;

namespace PetHotel.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddAutoMapper(applicationAssembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        services.AddScoped<IUserContext, UserContext>();
        services.AddHttpContextAccessor();
    } 
}
