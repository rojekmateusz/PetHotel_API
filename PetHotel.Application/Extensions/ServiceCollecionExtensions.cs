using Microsoft.Extensions.DependencyInjection;

namespace PetHotel.Application.Extensions;

public static class ServiceCollecionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
    }
}
