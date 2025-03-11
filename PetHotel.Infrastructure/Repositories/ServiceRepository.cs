using PetHotel.Domain.Entities;
using PetHotel.Domain.Repositories;
using PetHotel.Infrastructure.Persistance;

namespace PetHotel.Infrastructure.Repositories;

internal class ServiceRepository(PetHotelDbContext dbContext) : IServiceRepository
{
    public async Task<int> CreateService(Service entity)
    {
        dbContext.Services.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.ServiceId;
    }
}
